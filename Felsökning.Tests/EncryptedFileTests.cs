// ----------------------------------------------------------------------
// <copyright file="EncryptedFileTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
using System.Text;

namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class EncryptedFileTests
    {
        [TestMethod]
        [Platform(Include = "Win")]
        public void EncrypedFile_ctor()
        {
            var filePath = $"{Environment.CurrentDirectory}/Test.file";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            // Prevent lock contention after th creation.
            Thread.Sleep(1000);

            using (var result = new EncryptedFile(filePath))
            {
                result.Should().NotBeNull();
                result.Should().BeOfType<EncryptedFile>();

                using (var stream = result.DecryptAndOpen())
                {
                    var contentString = "This is some secret string.";
                    var contentBytes = ASCIIEncoding.ASCII.GetBytes(contentString);
                    stream.Write(contentBytes);
                }
            }

            var fileAttributes = File.GetAttributes(filePath);
            fileAttributes.Should().Be(FileAttributes.Encrypted);

            File.Delete(filePath);
        }

        [TestMethod]
        [Platform(Include = "Win")]
        public void EncrypedFile_ctor_Throws_ArgumentException()
        {
            var result = Assert.ThrowsException<ArgumentException>(() =>
            {
                _ = new EncryptedFile(string.Empty);
            });

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentException>();
        }

        [TestMethod]
        [Platform(Include = "Win")]
        public void EncrypedFile_ctor_Throws_FileNotFoundException()
        {
            var result = Assert.ThrowsException<FileNotFoundException>(() =>
            {
                _ = new EncryptedFile("Something");
            });

            result.Should().NotBeNull();
            result.Should().BeOfType<FileNotFoundException>();
        }
    }
}