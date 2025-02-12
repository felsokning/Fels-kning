// ----------------------------------------------------------------------
// <copyright file="FileExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FileExtensionsTests
    {
        [TestMethod]
        public async Task FileExtensions_ReadAllTextAsync_Succeeds()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { 
                    @"/dummyFilePath", 
                    new MockFileData("{\r\n  \"by\" : \"vinnyglennon\",\r\n  \"descendants\" : 36,\r\n  \"id\" : 32649091,\r\n  \"kids\" : [ 32650627, 32652889, 32651364, 32651377, 32655335, 32652375, 32652023, 32649456, 32651630, 32649718, 32653799, 32650386, 32657988, 32650124 ],\r\n  \"score\" : 162,\r\n  \"time\" : 1661859463,\r\n  \"title\" : \"Wikipedia Recent Changes Map\",\r\n  \"type\" : \"story\",\r\n  \"url\" : \"http://rcmap.hatnote.com/#en\"\r\n}") }
            });

            var result = await fileSystem.File.ReadAllTextAsync<SampleJson>("/dummyFilePath");

            result.Should().NotBeNull();
            result.Should().BeOfType<SampleJson>();
        }
    }
}