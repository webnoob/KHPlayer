using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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

        public List<ScreenDevice> GetAvailableScreens()
        {
            var result = new List<ScreenDevice>();

            for (var i = 0; i < Screen.AllScreens.Count(); i++)
            {
                var screen = Screen.AllScreens[i];
                result.Add(new ScreenDevice
                {
                    Id = i,
                    DeviceName = screen.DeviceName
                });
            }

            return result;
        }

        public PlayerScreen GetByName(string screenName)
        {
            if (string.IsNullOrEmpty(screenName))
                return null;
            
            return Get().FirstOrDefault(s => s.FriendlyName.Equals(screenName, StringComparison.OrdinalIgnoreCase));
        }

        public void Insert(PlayerScreen screen)
        {
            ScreenCache.Screens.Add(screen);
        }

        public void Delete(PlayerScreen screen)
        {
            ScreenCache.Screens.Remove(screen);
        }

        public PlayerScreen Create(string screenName, int screenNumber, AudioDevice audioDevice, ScreenDevice screenDevice)
        {
            return new PlayerScreen
            {
                Guid = Guid.NewGuid().ToString(),
                FriendlyName = screenName,
                AudioDevice = audioDevice
            };
        }

        public void AddScreen(string screenName, int screenNumber, AudioDevice audioDevice, ScreenDevice screenDevice)
        {
            Insert(Create(screenName, screenNumber, audioDevice, screenDevice));
        }
    }
}
