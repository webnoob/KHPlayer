using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using TagLib;
using File = TagLib.File;

namespace KHPlayer.Services
{
    public class FileTagService
    {
        private static List<PlaylistItemFile> _cachedPlaylistItemFiles;

        public FileTagService()
        {
            if (_cachedPlaylistItemFiles == null)
                _cachedPlaylistItemFiles = new List<PlaylistItemFile>();
        }

        public PlaylistItemFile GetTag(string filePath)
        {
            //No point loading the file again if we've already got the information.
            var playlistItemFile =
                _cachedPlaylistItemFiles.FirstOrDefault(
                    c => c.FileName.Equals(filePath, StringComparison.OrdinalIgnoreCase));

            if (playlistItemFile != null)
                return playlistItemFile;

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    try
                    {
                        //TODO: Find out how to use the File.AddFileResolver as this means we can handle the custom file types we're currently catching.
                        var file = File.Create(new StreamFileAbstraction(filePath, fileStream, fileStream));
                        
                        //This won't be hit if the file doesn't exist.
                        playlistItemFile = new PlaylistItemFile
                        {
                            FileName = filePath,
                            Type = file is TagLib.Mpeg.AudioFile ? PlayListItemType.Audio : PlayListItemType.Video,
                            Tag = new PlayListItemFileTag
                            {
                                Title = file.Tag.Title,
                                Track = file.Tag.Track
                            }
                        };

                        foreach (var picture in file.Tag.Pictures)
                        {
                            playlistItemFile.Tag.Pictures.Add(new PlayListItemFileTagImage
                            {
                                MimeType = picture.MimeType,
                                Data = new PlayListItemFileTagImageData
                                {
                                    Data = picture.Data.Data
                                }
                            });
                        }

                        _cachedPlaylistItemFiles.Add(playlistItemFile);
                        return playlistItemFile;
                    }
                    catch (UnsupportedFormatException)
                    {
                        //Ignore as we will create the file in TryMakeFileTag below.
                    }
                }

                playlistItemFile = TryMakeFileTag(filePath);
                _cachedPlaylistItemFiles.Add(playlistItemFile);
                return playlistItemFile;
            }
            catch (IOException)
            {
                //If the file is in use (already playing) then we don't want to throw an error.
                //This will however prevent the same file being added to the play list when it's currently playing
                //I can live with that ...
                return null;
            }
        }

        private PlaylistItemFile TryMakeFileTag(string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".pdf":
                    return new PdfService().GetTag(filePath);
            }
            return null;
        }
    }
}
