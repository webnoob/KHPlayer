using System;
using System.IO;
using KHPlayer.Classes;
using KHPlayer.Properties;
using TagLib;
using File = TagLib.File;

namespace KHPlayer.Services
{
    public class SongService
    {
        public string GetSongFile(string songStr)
        {
            var songNumber = GetSongNumber(songStr);
            return GetSongFile(songNumber);
        }

        private int GetSongNumber(string songStr)
        {
            int number;
            if (int.TryParse(songStr, out number)) 
                return number;

            return 0;
        }

        public string GetSongFile(int songNumber)
        {
            var allSongFiles = Directory.GetFiles(PathHelper.GetApplicationPath() + "\\" + Settings.Default.SongLocation);

            foreach (var file in allSongFiles)
            {
                try
                {
                    using (var fileStream = new FileStream(file, FileMode.Open))
                    {
                        var tagFile = File.Create(new StreamFileAbstraction(file, fileStream, fileStream));
                        if (tagFile.Tag.Track == songNumber)
                            return file;
                    }
                }
                //If the file is in use (already playing) then we don't want to throw an error.
                //This will however prevent the same file being added to the play list when it's currently playing
                //I can live with that ...
                catch (IOException) { }
            }


            return "";
        }

        public string GetSongUrl(string songStr)
        {
            return String.Format("http://download.jw.org/files/media_music/b9/iasn_E_00{0}.mp3", songStr);
        }
    }
}
