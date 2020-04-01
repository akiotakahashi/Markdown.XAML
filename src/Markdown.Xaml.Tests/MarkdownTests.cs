﻿using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Markup;
using System.Xml;

using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Markdown.Xaml.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class MarkdownTests
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenTest1_generatesExpectedResult()
        {
            var text = LoadText("Test1.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenTest2_generatesExpectedResult()
        {
            var text = LoadText("Test1.md");
            var markdown = new Markdown();
            markdown.DisabledTag = true;
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenLists_generatesExpectedResult()
        {
            var text = LoadText("Lists.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenTables_generatesExpectedResult() {
            var text = LoadText("Tables.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenTables1_generatesExpectedResult()
        {
            var text = LoadText("Tables.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenTables2_generatesExpectedResult()
        {
            var text = LoadText("Tables.md");
            var markdown = new Markdown();
            markdown.DisabledTag = true;
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenHorizontalRules_generatesExpectedResult()
        {
            var text = LoadText("HorizontalRules.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenLinksInline_generatesExpectedResult()
        {
            var text = LoadText("Links_inline_style.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenLinksInline2_generatesExpectedResult()
        {
            var text = LoadText("Links_inline_style.md");
            var markdown = new Markdown();
            markdown.DisabledTootip = true;
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenTextStyles_generatesExpectedResult()
        {
            var text = LoadText("Text_style.md");
            var markdown = new Markdown();
            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenImages_generatesExpectedResult()
        {
            var text = LoadText("Images.md");
            var markdown = new Markdown();
            markdown.AssetPathRoot = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenImages2_generatesExpectedResult()
        {
            var text = LoadText("Images.md");
            var markdown = new Markdown();
            markdown.DisabledLazyLoad = true;
            markdown.AssetPathRoot = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenImages3_generatesExpectedResult()
        {
            var text = LoadText("Images.md");
            var markdown = new Markdown();
            markdown.DisabledTootip = true;
            markdown.AssetPathRoot = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenBlockqoute_generatesExpectedResult()
        {
            var text = LoadText("Blockquite.md");
            var markdown = new Markdown();

            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Transform_givenMixing_generatesExpectedResult()
        {
            var text = LoadText("Mixing.md");
            var markdown = new Markdown();

            var result = markdown.Transform(text);
            Approvals.Verify(AsXaml(result));
        }

        private string LoadText(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly()
                               .GetManifestResourceStream("Markdown.Xaml.Tests." + name))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private string AsXaml(object instance)
        {
            using (var writer = new StringWriter())
            {
                var settings = new XmlWriterSettings { Indent = true };
                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {
                    XamlWriter.Save(instance, xmlWriter);
                }

                writer.WriteLine();
                return writer.ToString();
            }
        }
    }
}
