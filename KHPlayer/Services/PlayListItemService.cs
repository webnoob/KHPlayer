using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Helpers;

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

        public IEnumerable<PlayListItem> Get()
        {
            return _playListService.Get().SelectMany(p => p.Items);
        }

        public IEnumerable<PlayListItem> Get(PlayList playList)
        {
            if (playList == null)
                return null;

            return _playListService.GetByName(playList.Name).Items;
        }

        public PlayListItem GetByGuid(string guid)
        {
            return Get().FirstOrDefault(p => p.Guid == guid);
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
            var isStreamed = filePath.ToLower().Contains("http://") || 
                filePath.ToLower().Contains("https://") ||
                filePath.ToLower().Contains("ftp://");

            var plItem = new PlayListItem
            {
                Guid = Guid.NewGuid().ToString(),
                FileName = Path.GetFileName(filePath),
                FilePath = filePath,
                PlayListGuid = playList.Guid,
                Source = isStreamed ? PlayListItemSource.Streamed : PlayListItemSource.Disk,
            };

            plItem.ThumbnailPath = _thumbnailService.GenerateForFile(filePath, plItem.Source);
            if (PathHelper.FileIsLocked(filePath))
                return null;

            var tagFile = _fileTagService.GetTag(plItem.FilePath, plItem.Source);
            if (tagFile == null)
                return null;

            plItem.Type = tagFile.Type;
            plItem.TagName = tagFile.Tag.Title;
            plItem.FullScreen = plItem.Type != PlayListItemType.Audio;
            
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

        public void AddToPlaylistUsingUrl(string url, PlayList playList)
        {
            var playListItem = GetByFileName(url, playList) ?? Create(url, playList);

            if (playListItem != null)
                Insert(playListItem, playList);
        }
    }
}
