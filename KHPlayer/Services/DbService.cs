using System.Collections.Generic;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Data;
using KHPlayer.Extensions;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public class DbService
    {
        public void Save(bool confirmedSave)
        {
            Settings.Default.SavedScreens = ScreenCache.Screens.ToSerializedJson();

            if (confirmedSave)
            {
                Settings.Default.SavedData = PlayListCache.PlayLists.ToSerializedJson();
            }
        }

        public void Load()
        {
            PlayListCache.PlayLists = Settings.Default.SavedData.ToDeserialisedJson<List<PlayList>>();
            ScreenCache.Screens = Settings.Default.SavedScreens.ToDeserialisedJson<List<PlayerScreen>>();
        }
    }
}
