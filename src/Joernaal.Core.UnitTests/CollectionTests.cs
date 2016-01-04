using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Joernaal
{
    [TestFixture]
    public class CollectionTests
    {
        private const string FakeName = "Blog";
        private const string FakeDescription = "Articles part of a blog";

        [Test]
        public void Deserialize()
        {
            // Arrange
            var contents = "{\"Metadata\":{\"Name\":\"" + FakeName + "\",\"Description\":\"" + FakeDescription + "\"}}";
            var reader = new StringReader(contents);

            // Act
            var collection = Item.Deserialize<Collection>(reader);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual(FakeName, collection.Metadata["Name"]);
            Assert.AreEqual(FakeDescription, collection.Metadata["Description"]);
        }

        [Test]
        public void Serialize()
        {
            // Arrange
            var collection = new Collection
            {
                Metadata = new Dictionary<string, string>
                {
                    {"Name", FakeName},
                    {"Description", FakeDescription}
                }
            };
            var stringWriter = new StringWriter();

            // Act
            collection.Serialize(stringWriter);
            var text = stringWriter.ToString();

            // Assert
            Assert.IsNotNull(text);
            Assert.IsNotEmpty(text);
            Assert.AreEqual(
                "{\"Metadata\":{\"Name\":\"" + FakeName + "\",\"Description\":\"" + FakeDescription + "\"}}", text, text);
        }
    }
}