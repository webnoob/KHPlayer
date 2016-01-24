﻿using System;
using System.Collections.Generic;

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

        public PlayerScreen Screen { get; set; }
        
        public PlayListItemType Type { get; set; }

        public PlayListItemState State { get; set; }

        public PlayListItemSource Source { get; set; }

        public bool SupportsMultiCast { get { return Type == PlayListItemType.Video; } }
    }

    public enum PlayListItemState
    {
        Queued,
        Played
    }

    public enum PlayListItemType
    {
        Video,
        Audio,
        Pdf
    }

    public enum PlayListItemSource
    {
        Disk,
        Streamed
    }
}
