﻿using System;
using System.Collections.Generic;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Data;

namespace KHPlayer.Services
{
    public class PlayListService
    {
        private readonly ScreenService _screenService;

        public PlayListService()
        {
            _screenService = new ScreenService();
        }

        public IEnumerable<PlayList> Get()
        {
            var playLists = PlayListCache.PlayLists;
            foreach (var playListItem in playLists.SelectMany(playList => playList.Items))
            {
                playListItem.Screen = _screenService.GetByGuid(playListItem.ScreenGuid) ??
                                      _screenService.GetDefaultScreen();
            }
            return playLists;
        }

        public PlayList GetByGuid(string guid)
        {
            return Get().FirstOrDefault(p => p.Guid == guid);
        }

        public PlayList Insert(PlayList playList)
        {
            if (Get().FirstOrDefault(p => p.Name.Equals(playList.Name, StringComparison.OrdinalIgnoreCase)) == null)
            {
                PlayListCache.PlayLists.Add(playList);
            }

            return playList;
        }

        public void Update(PlayList playList)
        {
            Delete(playList);
            Insert(playList);
        }

        public void Delete(PlayList playList)
        {
            PlayListCache.PlayLists.Remove(playList);
        }

        public PlayList GetByName(string name)
        {
            return Get().FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public PlayList Create(string playListName)
        {
            return new PlayList
            {
                Guid = Guid.NewGuid().ToString(),
                Name = playListName
            };
        }

        public void MoveItem(PlayListItem playListItem, bool up)
        {
            var playList = GetByGuid(playListItem.PlayListGuid);
            var currentIndex = playList.Items.IndexOf(playListItem);
            var newIndex = up ? currentIndex - 1 : currentIndex + 1;
            if ((newIndex < 0) || (newIndex > playList.Items.Count))
                return;

            //If we are moving the last item down 1, then when the remove call is made on the list, the item count will be -1
            if (newIndex == playList.Items.Count)
                newIndex = newIndex - 1;

            playList.Items.Remove(playListItem);
            playList.Items.Insert(newIndex, playListItem);
        }

        public IEnumerable<PlayListItem> GetAllPlayListItemsInGroup(PlayListItem playListItem)
        {
            if (playListItem.Group == 0)
                return new[] {playListItem};

            var playList = GetByGuid(playListItem.PlayListGuid);
            return playList.Items.Where(p => p.Group == playListItem.Group);
        }
    }
}
