using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Extensions;
using TagLib;
using File = System.IO.File;

namespace KHPlayer.Services
{
    public class PlayListItemService
    {
        private readonly PlayListService _playListService;
        private readonly ThumbnailService _thumbnailService;
        private readonly FileTagService _fileTagService;

        public PlayListItemService()
        {
            _playListService = new PlayListService();
            _thumbnailService = new ThumbnailService();
            _fileTagService = new FileTagService();
        }

        public IEnumerable<PlayListItem> Get(PlayList playList)
        {
            if (playList == null)
                return null;

            return _playListService.GetByName(playList.Name).Items;
        }

        public PlayListItem GetByFileName(string fileName, PlayList playList)
        {
            if (playList == null)
                return null;

            return Get(playList).FirstOrDefault(pi => pi.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase));
        }

        public void Insert(PlayListItem playListItem, PlayList playList)
        {
            if (!playList.Items.Contains(playListItem))
                playList.Items.Add(playListItem);
        }

        public void Delete(PlayListItem playListItem)
        {
            var playList = _playListService.GetByGuid(playListItem.PlayListGuid);
            if (playList == null)
                return;

            playList.Items.Remove(playListItem);
        }

        public PlayListItem Create(string filePath, PlayList playList)
        {
            var plItem = new PlayListItem
            {
                Guid = Guid.NewGuid().ToString(),
                FileName = Path.GetFileName(filePath),
                FilePath = filePath,
                PlayListGuid = playList.Guid,
                ThumbnailPath = _thumbnailService.GenerateForFile(filePath)
            };

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.IsLocked())
                return null;

            var tagFile = _fileTagService.GetTag(plItem.FilePath);
            if (tagFile == null)
                return null;

            plItem.Type = tagFile is TagLib.Mpeg.AudioFile ? PlayListItemType.Audio : PlayListItemType.Video;
            plItem.TagName = String.Format("[{0}] - {1}", plItem.Type, tagFile.Tag.Title);
            
            return plItem;
        }

        public void AddToPlaylistUsingFilePath(string path, PlayList playList)
        {
            if (File.Exists(path))
            {
                var playListItem = GetByFileName(path, playList) ?? Create(path, playList);

                if (playListItem != null)
                    Insert(playListItem, playList);
            }
        }

        public void AddRangeToPlaylist(string[] filePaths, PlayList playList)
        {
            foreach (var filePath in filePaths)
                AddToPlaylistUsingFilePath(filePath, playList);
        }
    }
}
