using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public class SongService
    {
        private readonly FileTagService _fileTagService;
        private readonly List<int> _playedRandomSongs; 
        private readonly int _totalSongCount;

        public SongService()
        {
            _fileTagService = new FileTagService();
            _playedRandomSongs = new List<int>();
            _totalSongCount = Directory.GetFiles(Settings.Default.SongLocation).Count();
        }

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
                var tagFile = _fileTagService.GetTag(file);
                if (tagFile == null)
                    continue;

                if (tagFile.Tag.Track == songNumber)
                    return file;
            }

            return "";
        }

        public string GetSongUrl(string songStr)
        {
            return String.Format("http://download.jw.org/files/media_music/b9/iasn_E_00{0}.mp3", songStr);
        }

        public int GetRandomSongNumer()
        {
            var possibleSongs = GetPossibleSongs();
            if (!possibleSongs.Any())
                return 0;
            
            var rnd = new Random();
            var index = rnd.Next(0, possibleSongs.Count);
            var songNum = possibleSongs[index];
            _playedRandomSongs.Add(songNum);
            return songNum;
        }

        private List<int> GetPossibleSongs()
        {
            var possibleSongs = new List<int>();
            if (_totalSongCount == 0)
                return possibleSongs;

            for (var i = 1; i <= _totalSongCount; i++)
                if (!_playedRandomSongs.Contains(i))
                    possibleSongs.Add(i);

            if (possibleSongs.Any()) 
                return possibleSongs;

            //If we don't have any songs left to play, clear the played list and start the random process again.
            _playedRandomSongs.Clear();
            possibleSongs = GetPossibleSongs();
            return possibleSongs;
        }
    }
}
