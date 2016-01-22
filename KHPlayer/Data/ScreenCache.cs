using System.Collections.Generic;
using KHPlayer.Classes;

namespace KHPlayer.Data
{
    public static class ScreenCache
    {
        private static List<PlayerScreen> _screens;

        public static List<PlayerScreen> Screens
        {
            get { return _screens ?? (_screens = new List<PlayerScreen>()); }
            set { _screens = value; }
        }
    }
}
