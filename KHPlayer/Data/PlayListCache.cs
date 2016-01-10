using System.Collections.Generic;
using KHPlayer.Classes;

namespace KHPlayer.Data
{
    public class PlayListCache
    {
        private static List<PlayList> _playLists;

        public static List<PlayList> PlayLists
        {
            get { return _playLists ?? (_playLists = new List<PlayList>()); }
            set { _playLists = value; }
        }
    }
}
