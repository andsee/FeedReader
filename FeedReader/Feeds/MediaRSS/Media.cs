using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CodeHollow.FeedReader.Feeds.MediaRSS
{

    /// <summary>
    /// Media object
    /// </summary>
    public class Media
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// Reads a rss feed item enclosure based on the xml given in element
        /// </summary>
        /// <param name="element">enclosure element as xml</param>
        public Media(XElement element, Media baseMedia = null)
        {
            this.Url = element.GetAttributeValue("url");
            this.FileSize = Helpers.TryParseInt(element.GetAttributeValue("fileSize"));
            this.Type = element.GetAttributeValue("type");
            this.Medium  = Helpers.TryParseMedium(element.GetAttributeValue("medium"));
            this.isDefault = Helpers.TryParseBool(element.GetAttributeValue("isDefault"));
            this.Duration = Helpers.TryParseInt(element.GetAttributeValue("duration"));
            this.Height = Helpers.TryParseInt(element.GetAttributeValue("height"));
            this.Width = Helpers.TryParseInt(element.GetAttributeValue("width"));
            this.Language = element.GetAttributeValue("lang");

            var thumbnails = element.GetElements("media", "thumbnail");
            this.Thumbnails = thumbnails.Select(x => new Thumbnail(x)).ToList();
            if (baseMedia != null)
            {
                if (string.IsNullOrWhiteSpace(this.Url)) this.Url = baseMedia.Url;
                if (!this.FileSize.HasValue) this.FileSize = baseMedia.FileSize;
                if (string.IsNullOrWhiteSpace(this.Type)) this.Type = baseMedia.Type;
                if (!this.isDefault.HasValue) this.isDefault = baseMedia.isDefault;
                if (!this.Duration.HasValue) this.Duration = baseMedia.Duration;
                if (!this.Height.HasValue) this.Height = baseMedia.Height;
                if (!this.Width.HasValue) this.Width = baseMedia.Width;
                if (string.IsNullOrWhiteSpace(this.Language)) this.Language = baseMedia.Language;
                if (Thumbnails.Count == 0) this.Thumbnails = baseMedia.Thumbnails;
            }

        }

        /// <summary>
        /// The direct URL to the media object
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Number of bytes of the media object. Optional attribute
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Standard MIME type of the object. Optional attribute
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Type of object. Optional attribute
        /// </summary>
        public Medium Medium { get; set; }


        /// <summary>
        /// Determines if this is the default object that should be used for the <see cref="MediaGroup"/>
        /// </summary>
        public bool? isDefault { get; set; }

        /// <summary>
        /// Number of seconds the media object plays. Optional attribute
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Height of the object media. Optional attribute
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Width of the object media. Optional attribute
        /// </summary>
        public int? Width { get; set; }

    
        /// <summary>
        /// The primary language encapsulated in the media object. Language codes possible are detailed in RFC 3066. This attribute is used similar to the xml:lang attribute detailed in the XML 1.0 Specification (Third Edition). It is an optional attribute.
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// Representative images for the media object
        /// </summary>
        public ICollection<Thumbnail> Thumbnails { get; set; }

    }


}
