using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Joernaal.Tasks;
using NUnit.Framework;

namespace Joernaal
{
    [TestFixture]
    public class ProjectTests
    {
        private const string FakeName = "Blog";
        private const string FakeDescription = "Articles part of a blog";

        [Test]
        public void Deserialize()
        {
            // Arrange
            var contents =
                "{\"Tasks\":{\"Clean\":{\"$type\":\"Clean\"},\"Analyze\":{\"$type\":\"Analyze\"}},\"Metadata\":{\"Name\":\"" +
                FakeName + "\",\"Description\":\"" + FakeDescription + "\"}}";
            var reader = new StringReader(contents);

            // Act
            var project = Item.Deserialize<Project>(reader);

            // Assert
            Assert.IsNotNull(project);
            Assert.AreEqual(FakeName, project.Metadata["Name"]);
            Assert.AreEqual(FakeDescription, project.Metadata["Description"]);
            Assert.IsNotNull(project.Tasks);
            Assert.AreEqual(2, project.Tasks.Count);
            Assert.IsInstanceOf<CleanTask>(project.Tasks[project.Tasks.Keys.ElementAt(0)]);
            Assert.IsInstanceOf<AnalyzeTask>(project.Tasks[project.Tasks.Keys.ElementAt(1)]);
        }

        [Test]
        public void Serialize()
        {
            // Arrange
            var project = new Project
            {
                Key = "/a/b/c/",
                Metadata = new Dictionary<string, string>
                {
                    {"Name", FakeName},
                    {"Description", FakeDescription}
                },
                Tasks = new Dictionary<string, Task>()
                {
                    {"Clean", new CleanTask() },
                    {"Analyze", new AnalyzeTask()},
                }
            };
            var stringWriter = new StringWriter();

            // Act
            project.Serialize(stringWriter);
            var text = stringWriter.ToString();

            // Assert
            Assert.IsNotNull(text);
            Assert.IsNotEmpty(text);
            Assert.AreEqual(
                "{\"Tasks\":{\"Clean\":{\"$type\":\"Clean\"},\"Analyze\":{\"$type\":\"Analyze\"}},\"Metadata\":{\"Name\":\"" + FakeName + "\",\"Description\":\"" + FakeDescription + "\"}}", text, text);
            Console.WriteLine(text);    
        }

        [TestCase("TestData\\Project1")]
        public void Execute(string path)
        {
            // Arrange
            path = Path.Combine(TestContext.CurrentContext.TestDirectory, path);
            var project = Project.Create(path);

            // Act
            project.Execute();

            // Assert
        }
    }
}