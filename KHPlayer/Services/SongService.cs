﻿using System;
using System.IO;
using KHPlayer.Classes;
using KHPlayer.Properties;
using TagLib;
using File = TagLib.File;

namespace KHPlayer.Services
{
    public class SongService
    {
        private readonly FileTagService _fileTagService;

        public SongService()
        {
            _fileTagService = new FileTagService();
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
    }
}
