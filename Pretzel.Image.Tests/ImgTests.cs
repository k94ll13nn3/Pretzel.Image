using System;
using DotLiquid;
using NUnit.Framework;

namespace Pretzel.Image.Tests
{
    [TestFixture]
    public class ImgTests
    {
        [Test]
        public void TestGoodPatterns()
        {
            Template.RegisterTag<ImageTag>("img");

            var templateOk1 = Template.Parse("{% img path %}");
            var templateOk1b = Template.Parse("{% img path      %}");
            var templateOk1c = Template.Parse("{%      img path %}");
            var templateOk1d = Template.Parse("{%      img path      %}");
            var templateOk2 = Template.Parse("{% img \"path to img\" %}");
            var templateOk3 = Template.Parse("{% img path 300  %}");
            var templateOk4 = Template.Parse("{% img path 300 200 %}");
            var templateOk5 = Template.Parse("{% img path center %}");
            var templateOk6 = Template.Parse("{% img path center 300 200 %}");
            var templateOk7 = Template.Parse("{% img path \"center class\" 300 200 %}");
            var templateOk8 = Template.Parse("{% img \"path to img\" \"center class\" 300 200 %}");

            Assert.AreEqual("<img src=\"path\"></img>", templateOk1.Render());
            Assert.AreEqual("<img src=\"path\"></img>", templateOk1b.Render());
            Assert.AreEqual("<img src=\"path\"></img>", templateOk1c.Render());
            Assert.AreEqual("<img src=\"path\"></img>", templateOk1d.Render());
            Assert.AreEqual("<img src=\"path to img\"></img>", templateOk2.Render());
            Assert.AreEqual("<img src=\"path\" width=300></img>", templateOk3.Render());
            Assert.AreEqual("<img src=\"path\" width=300 height=200></img>", templateOk4.Render());
            Assert.AreEqual("<img class=\"center\" src=\"path\"></img>", templateOk5.Render());
            Assert.AreEqual("<img class=\"center\" src=\"path\" width=300 height=200></img>", templateOk6.Render());
            Assert.AreEqual("<img class=\"center class\" src=\"path\" width=300 height=200></img>", templateOk7.Render());
            Assert.AreEqual("<img class=\"center class\" src=\"path to img\" width=300 height=200></img>", templateOk8.Render());
        }

        [Test]
        public void TestWrongPatterns()
        {
            Template.RegisterTag<ImageTag>("img");

            Assert.Throws<ArgumentException>(() => Template.Parse("{% img %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path 200 25A %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path 200 25A class %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path class class %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% img path 200 class %}"));
        }
    }
}