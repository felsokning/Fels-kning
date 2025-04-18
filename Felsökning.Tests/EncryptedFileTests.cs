// ----------------------------------------------------------------------
// <copyright file="EncryptedFileTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
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
            // Not testable on Linux.
            if (!OperatingSystem.IsLinux())
            {
                var filePath = $"{Environment.CurrentDirectory}/Test.file";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

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
                fileAttributes.Should().Be(FileAttributes.Archive | FileAttributes.Encrypted);

                using (var encryptedFileResult = new EncryptedFile(filePath))
                {

                    encryptedFileResult.Should().NotBeNull();
                    encryptedFileResult.Should().BeOfType<EncryptedFile>();

                    using (var fileStream = encryptedFileResult.DecryptAndOpen())
                    {
                        fileStream.Should().NotBeNull();
                        fileStream.Should().BeOfType<FileStream>();
                    }
                }

                File.Delete(filePath);
            }
        }

        [TestMethod]
        [Platform(Include = "Win")]
        public void EncrypedFile_ctor_Throws_ArgumentException()
        {
            var result = Assert.ThrowsExactly<ArgumentException>(() =>
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
            var result = Assert.ThrowsExactly<FileNotFoundException>(() =>
            {
                _ = new EncryptedFile("Something");
            });

            result.Should().NotBeNull();
            result.Should().BeOfType<FileNotFoundException>();
        }
    }
}