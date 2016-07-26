using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace KHPlayer.Classes
{
    public class PlayList
    {
        private List<PlayListItem> _items;

        public string Guid { get; set; }

        public string Name { get; set; }

        public List<PlayListItem> Items
        {
            get { return _items ?? (_items = new List<PlayListItem>()); }
            set { _items = value; }
        }
    }

    public class PlayListItem
    {
        public string Guid { get; set; }
        
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string ThumbnailPath { get; set; }

        public string PlayListGuid { get; set; }

        public string TagName { get; set; }

        public int Group { get; set; }

        public bool SupportsMultiCast { get { return Type == PlayListItemType.Video; } }

        public string ScreenGuid { get; set; }

        public bool FullScreen { get; set; }

        public PlayListItemSource Source { get; set; }

        public PlayListItemType Type { get; set; }

        [JsonIgnore]
        public string CurrentTime { get; set; }

        [JsonIgnore]
        public string MaxTime { get; set; }

        [JsonIgnore]
        public string Time
        {
            get
            {
                return string.IsNullOrEmpty(CurrentTime) 
                    ? "0:00" 
                    : string.Format("{0} / {1}", CurrentTime, MaxTime);
            }
        }

        [JsonIgnore]
        public PlayerScreen Screen { get; set; }

        [JsonIgnore]
        public PlayListItemState State { get; set; }

        public int PdfPageNumber { get; set; }

        public string PdfView { get; set; }
        
        public int StartMin { get; set; }
        public int StartSec { get; set; }
        
        [JsonIgnore]
        public string StartTime { get { return string.Format("{0:D2}:{1:D2}", StartMin, StartSec); } }

        public int StopMin { get; set; }
        public int StopSec { get; set; }

        [JsonIgnore]
        public string StopTime { get { return string.Format("{0:D2}:{1:D2}", StopMin, StopSec); } }
    }

    public enum PlayListItemState
    {
        Queued,
        Played,
        Playing
    }

    public enum PlayListItemType
    {
        Video,
        Audio,
        Pdf,
        Image
    }

    public enum PlayListItemSource
    {
        Disk,
        Streamed
    }
}
