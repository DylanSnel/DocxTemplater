﻿using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;


namespace DocxTemplater.Test
{
    internal static class TestHelper
    {
        private static int s_openCounter = 0;

        public static void SaveAsFileAndOpenInWord(this Stream stream, string extension = "docx")
        {
#if RELEASE
            return;
#pragma warning disable CS0162
#endif
            if (Environment.GetEnvironmentVariable("DOCX_TEMPLATER_VISUAL_TESTING") == null || s_openCounter > 1)
            {
                return;
            }

            s_openCounter++;
            stream.Position = 0;
            var fileName = Path.ChangeExtension(Path.GetTempFileName(), extension);
            using (var fileStream = File.OpenWrite(fileName))
            {
                stream.CopyTo(fileStream);
            }

            ProcessStartInfo psi = new()
            {
                FileName = fileName,
                UseShellExecute = true
            };
            using var proc = Process.Start(psi);
#pragma warning restore CS0162
        }

        public static string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }

        public static void ExpectXmlIsEqual(string actual, string expected)
        {
            var expectedParsed = XElement.Parse(expected);
            var actualParsed = XElement.Parse(actual);
            if (!XNode.DeepEquals(expectedParsed, actualParsed))
            {
                Assert.That(actualParsed.ToString(), Is.EqualTo(expectedParsed.ToString()));
            }
        }

    }


}
