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
        public void Save()
        {
            Settings.Default.SavedData = PlayListCache.PlayLists.ToSerializedJson();
        }

        public void Load()
        {
            PlayListCache.PlayLists = Settings.Default.SavedData.ToDeserialisedJson<List<PlayList>>();
        }
    }
}
