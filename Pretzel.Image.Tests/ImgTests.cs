using System;
using DotLiquid;
using NUnit.Framework;

namespace CustomTags.Tests
{
    [TestFixture]
    public class ImgTests
    {
        [Test]
        public void TestGoodPatterns()
        {
            Template.RegisterTag<Img>("img");

            Template templateOk1 = Template.Parse("{% img path %}");
            Template templateOk2 = Template.Parse("{% img \"path to img\" %}");
            Template templateOk3 = Template.Parse("{% img path 300  %}");
            Template templateOk4 = Template.Parse("{% img path 300 200 %}");
            Template templateOk5 = Template.Parse("{% img path center %}");
            Template templateOk6 = Template.Parse("{% img path \"center class\" 300 200 %}");

            Assert.AreEqual("<img src=\"path\"></img>", templateOk1.Render());
            Assert.AreEqual("<img src=\"path to img\"></img>", templateOk2.Render());
            Assert.AreEqual("<img src=\"path\" width=300></img>", templateOk3.Render());
            Assert.AreEqual("<img src=\"path\" width=300 height=200></img>", templateOk4.Render());
            Assert.AreEqual("<img class=\"center\" src=\"path\"></img>", templateOk5.Render());
            Assert.AreEqual("<img class=\"center class\" src=\"path\" width=300 height=200></img>", templateOk6.Render());
        }

        [Test]
        public void TestWrongPatterns()
        {
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path 200 25A %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path 200 25A class %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path class class %}"));
        }
    }
}