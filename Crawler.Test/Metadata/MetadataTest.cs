namespace Crawler.Test.Metadata
{
    using Business.Models;
    using NUnit.Framework;

    public class MetadataTest
    {
        [Test]
        public void ShouldReturnBytes()
        {
            Assert.That(Metadata.Bytes, Is.EqualTo("bytes"));
        }

        [Test]
        public void ShouldReturnIsDir()
        {
            Assert.That(Metadata.IsDir, Is.EqualTo("is_dir"));
        }

        [Test]
        public void ShouldReturnMimeType()
        {
            Assert.That(Metadata.MimeType, Is.EqualTo("mime_type"));
        }

        [Test]
        public void ShouldReturnPath()
        {
            Assert.That(Metadata.Path, Is.EqualTo("path"));
        }
    }
}
