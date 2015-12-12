// Pretzel.Image plugin
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.Image
{
    [Export(typeof(ITag))]
    public class ImageTag : Tag, ITag
    {
        private const string ExceptionMessage = "Expected syntax: {% img path/to/file [class(es)] [width [height]] %}";

        private string html;

        public new string Name => "Img";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            if (string.IsNullOrWhiteSpace(markup))
            {
                throw new ArgumentException(ExceptionMessage);
            }

            var attributes = new string[3];

            // The goal is to split the markup string in two : the part with the path and the part after the path.
            // This is surely not the best way to do it.
            var splittedMarkup = markup.Trim().SplitEnclosedInQuotes(new string[] { " " });

            if (!splittedMarkup.Any() || splittedMarkup.Count > 4)
            {
                throw new ArgumentException(ExceptionMessage);
            }

            switch (splittedMarkup.Count)
            {
                case 2:
                    attributes = ProcessOneArgument(splittedMarkup[1]);
                    break;

                case 3:
                    attributes = ProcessTwoArguments(splittedMarkup);
                    break;

                case 4:
                    attributes = ProcessThreeArguments(splittedMarkup);
                    break;
            }

            var srcAttribute = $"src=\"{splittedMarkup[0]}\"";

            this.html = $"<img {attributes[0]}{srcAttribute}{attributes[1]}{attributes[2]}></img>";
        }

        public override void Render(Context context, System.IO.TextWriter result)
        {
            result.Write(this.html);
        }

        private static string[] ProcessOneArgument(string markup)
        {
            var values = new string[3];
            int tmpStorage;

            if (int.TryParse(markup, out tmpStorage))
            {
                values[1] = $" width={markup}";
            }
            else
            {
                values[0] = $"class=\"{markup}\" ";
            }

            return values;
        }

        private static string[] ProcessThreeArguments(IList<string> markup)
        {
            var values = new string[3];

            int tmpStorage;

            if (int.TryParse(markup[1], out tmpStorage) || !int.TryParse(markup[2], out tmpStorage) || !int.TryParse(markup[3], out tmpStorage))
            {
                throw new ArgumentException(ExceptionMessage);
            }

            values[0] = $"class=\"{markup[1]}\" ";
            values[1] = $" width={markup[2]}";
            values[2] = $" height={markup[3]}";

            return values;
        }

        private static string[] ProcessTwoArguments(IList<string> markup)
        {
            var values = new string[3];

            int tmpStorage;

            // The second argument is either the width or the height.
            if (!int.TryParse(markup[2], out tmpStorage))
            {
                throw new ArgumentException(ExceptionMessage);
            }

            if (int.TryParse(markup[1], out tmpStorage))
            {
                values[1] = $" width={markup[1]}";
                values[2] = $" height={markup[2]}";
            }
            else
            {
                values[0] = $"class=\"{markup[1]}\" ";
                values[1] = $" width={markup[2]}";
            }

            return values;
        }
    }
}