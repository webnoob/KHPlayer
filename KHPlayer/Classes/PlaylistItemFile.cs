using System.Collections;
using System.Collections.Generic;
using TagLib;

namespace KHPlayer.Classes
{
    public class PlaylistItemFile
    {
        public string FileName { get; set; }

        public PlayListItemFileTag Tag { get; set; }

        public PlayListItemType Type { get; set; }
    }

    public class PlayListItemFileTag
    {
        private List<PlayListItemFileTagImage> _pictures;

        public string Title { get; set; }

        public uint Track { get; set; }

        public List<PlayListItemFileTagImage> Pictures
        {
            get { return _pictures ?? (_pictures = new List<PlayListItemFileTagImage>()); }
            set { _pictures = value; }
        }
    }

    public class PlayListItemFileTagImage
    {
        public string MimeType { get; set; }

        public PlayListItemFileTagImageData Data { get; set; }

        public string FilePath { get; set; }
    }

    public class PlayListItemFileTagImageData
    {
        public byte[] Data { get; set; }
    }
}