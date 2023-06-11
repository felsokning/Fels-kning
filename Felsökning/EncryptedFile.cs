// ----------------------------------------------------------------------
// <copyright file="EncryptedFile.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EncryptedFile"/> class.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class EncryptedFile : IDisposable
    {
        /// <summary>
        ///     The <see cref="Path"/> property is used to statically store the path to file for use 
        ///     throughout the class' lifetime.
        /// </summary>
        private static string Path { get; set;  } = string.Empty;

        /// <summary>
        ///     The <see cref="IsEncrypted"/> property is used to signify if we should bother with decrypting
        ///     the file before opening it.
        /// </summary>
        private static bool IsEncrypted { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncryptedFile"/> class, which is used to ensure
        ///     that the file is encrypted on disposal and decrypted when opened.
        /// </summary>
        /// <param name="path">The path where the file is located.</param>
        public EncryptedFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Path = path;
                if ((File.GetAttributes(Path) & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                {
                    IsEncrypted = true;
                }
            }
            else
            {
                throw new ArgumentException($"You've crossed the streams, Eagon! The path supplied for the EncryptedFile type was null or empy!");
            }
        }

        /// <summary>
        ///     Decrypts the file (if encrypted) and returns the <see cref="FileStream"/> from opening the file.
        /// </summary>
        /// <returns>The <see cref="FileStream"/> of the file.</returns>
        public FileStream DecryptAndOpen()
        {
            if (IsEncrypted)
            {
                File.Decrypt(Path); // Depends on the user having access to the certificate in the Trusted Store.
            }

            return File.Open(Path, FileMode.OpenOrCreate);
        }

        /// <summary>
        ///     Encrypts the file before exiting.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if ((File.GetAttributes(Path) & FileAttributes.Encrypted) != FileAttributes.Encrypted)
            {
                File.Encrypt(Path);
            }

            Path = string.Empty;

            GC.ReRegisterForFinalize(Path);
            GC.ReRegisterForFinalize(IsEncrypted);
        }
    }
}