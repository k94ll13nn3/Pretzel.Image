﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace CustomTags
{
    /// <summary>
    /// The image tag.
    /// </summary>
    public class ImageTag : Tag, ITag
    {
        /// <summary>
        /// The HTML string that will be rendered.
        /// </summary>
        private string html;

        /// <summary>
        /// Overrides the tag name.
        /// </summary>
        public new string Name => "Img";

        /// <inheritdoc/>
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            if (string.IsNullOrWhiteSpace(markup))
            {
                throw new ArgumentException("Expected syntax: {% img path/to/file [class(es)] [width [height]] %}");
            }

            string srcAttribute = string.Empty;
            string widthAttribute = string.Empty;
            string heightAttribute = string.Empty;
            string classAttribute = string.Empty;

            // The goal is to split the markup string in two : the part with the path and the part after the path.
            // This is surely not the best way to do it.
            IList<string> splittedMarkup = markup.Trim().SplitEnclosedInQuotes(new string[] { " " });

            if (splittedMarkup.Any() == false)
            {
                throw new ArgumentException("Expected syntax: {% img path/to/file [class(es)] [width [height]] %}");
            }

            // String.Join cannot be used because it does not recover the possibles quotes.
            string joined = markup.Replace(splittedMarkup[0], string.Empty).Replace("\"\"", string.Empty).Trim();

            srcAttribute = $"src=\"{splittedMarkup[0]}\"";
            var regex = new Regex(@"^(?:(""[A-z\s]+""|[A-z]+))?(?:\s(\d+)(?:\s(\d+)+)?)?$|^(?:(\d+)(?:\s(\d+)+)?)?$");
            var match = regex.Match(joined);
            if (match.Success)
            {
                if (match.Groups[1].Success)
                {
                    classAttribute = $"class=\"{match.Groups[1].Value.Replace("\"", string.Empty)}\" ";
                    if (match.Groups[2].Success)
                    {
                        widthAttribute = $" width={match.Groups[2].Value}";
                        if (match.Groups[3].Success)
                        {
                            heightAttribute = $" height={match.Groups[3].Value}";
                        }
                    }
                }
                else
                {
                    if (match.Groups[4].Success)
                    {
                        widthAttribute = $" width={match.Groups[4].Value}";
                        if (match.Groups[5].Success)
                        {
                            heightAttribute = $" height={match.Groups[5].Value}";
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Expected syntax: {% img path/to/file [class(es)] [width [height]] %}");
            }

            this.html = $"<img {classAttribute}{srcAttribute}{widthAttribute}{heightAttribute}></img>";
        }

        /// <inheritdoc/>
        public override void Render(Context context, System.IO.TextWriter result)
        {
            result.Write(this.html);
        }
    }
}