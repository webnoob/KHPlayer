using System;
using System.Collections.Generic;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Data;

namespace KHPlayer.Services
{
    public class ScreenService
    {
        public IEnumerable<PlayerScreen> Get()
        {
            return ScreenCache.Screens;
        }

        public PlayerScreen GetByName(string screenName)
        {
            return Get().FirstOrDefault(s => s.Name.Equals(screenName, StringComparison.OrdinalIgnoreCase));
        }

        public void Insert(PlayerScreen screen)
        {
            ScreenCache.Screens.Add(screen);
        }

        public void Delete(PlayerScreen screen)
        {
            ScreenCache.Screens.Remove(screen);
        }

        public PlayerScreen Create(string screenName, int screenNumber)
        {
            return new PlayerScreen
            {
                Guid = Guid.NewGuid().ToString(),
                Name = screenName,
                ScreenNumber = screenNumber
            };
        }

        public void AddScreen(string screenName, int screenNumber)
        {
            Insert(Create(screenName, screenNumber));
        }
    }
}
