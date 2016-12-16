using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Helpers;
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

        public string GetSongFile(string songStr, bool withLyrics)
        {
            var songNumber = GetSongNumber(songStr);
            return GetSongFile(songNumber, withLyrics);
        }

        private int GetSongNumber(string songStr)
        {
            int number;
            if (int.TryParse(songStr, out number)) 
                return number;

            return 0;
        }

        public string GetSongFile(int songNumber, bool withLyrics)
        {
            var songPath = PathHelper.GetApplicationPath() + "\\" + Settings.Default.SongLocation;
            var songPathWithLyrics = PathHelper.GetApplicationPath() + "\\" + Settings.Default.SongLocation + "\\WithLyrics";
            var newSongPathWithLyrics = PathHelper.GetApplicationPath() + "\\" + Settings.Default.SongLocation + "\\New";
            string[] allSongFiles;
            if (Settings.Default.UseNewSongs)
            {
                allSongFiles = withLyrics
                    ? Directory.GetFiles(songPathWithLyrics)
                    : Directory.GetFiles(newSongPathWithLyrics);
            }
            else
            {
                allSongFiles = Directory.GetFiles(songPath);
            }

            foreach (var file in allSongFiles)
                if (Path.GetFileNameWithoutExtension(file) == songNumber.ToString())
                    return file;

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

        public string GetRandomSongFilePath()
        {
            //If there are duplicate songs in the song folder with the same song number
            //make sure the song we return actually exists (i.e 215 songs in folder and it 
            //tries to load song 215, this won't exist.

            var songNum = GetRandomSongNumer();
            var songFilePath = GetSongFile(songNum, Settings.Default.UseLyricsOnRandomSongs);
            while (!File.Exists(songFilePath))
                songFilePath = GetRandomSongFilePath();

            return songFilePath;
        }

        public void RenameAllNewSongFolders()
        {
            string[] pathsToFix = new[] {
                PathHelper.GetApplicationPath() + "\\" + Settings.Default.SongLocation + "\\WithLyrics",
                PathHelper.GetApplicationPath() + "\\" + Settings.Default.SongLocation + "\\New"
            };

            foreach (string path in pathsToFix)
            {
                var files = Directory.GetFiles(path).ToList();
                foreach (var file in files)
                {
                    // Looks odd but we get the number as 001 (string) then convert to int to get 1, then back to string for file name :)
                    var songNumber = Convert.ToInt32(new string(Path.GetFileNameWithoutExtension(file).Replace("720P", "").Where(char.IsDigit).ToArray()));
                    var newFilePath = Path.Combine(Path.GetDirectoryName(file), songNumber.ToString() + Path.GetExtension(file));
                    if (!newFilePath.Equals(file, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Move(file, newFilePath);
                    }
                }
            }
        }
    }
}
