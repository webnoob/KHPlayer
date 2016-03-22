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

        public PlaylistItemFile GetTag(string filePath, PlayListItemSource source)
        {
            //No point loading the file again if we've already got the information.
            var playlistItemFile =
                _cachedPlaylistItemFiles.FirstOrDefault(
                    c => c != null && c.FileName.Equals(filePath, StringComparison.OrdinalIgnoreCase));

            if (playlistItemFile != null)
                return playlistItemFile;

            try
            {
                if (source == PlayListItemSource.Disk)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        try
                        {
                            //TODO: Find out how to use the File.AddFileResolver as this means we can handle the custom file types we're currently catching.
                            var file = File.Create(new StreamFileAbstraction(filePath, fileStream, fileStream));
                            var isCorrupt = file.CorruptionReasons != null && file.CorruptionReasons.Any();
                            var title = isCorrupt || file.Tag.Title == null ? Path.GetFileName(filePath) : file.Tag.Title;
                            var track = isCorrupt ? 0 : file.Tag.Track;

                            //This won't be hit if the file doesn't exist.
                            playlistItemFile = new PlaylistItemFile
                            {
                                FileName = filePath,
                                Type = file is TagLib.Mpeg.AudioFile
                                     ? PlayListItemType.Audio : PlayListItemType.Video,
                                Tag = new PlayListItemFileTag
                                {
                                    Title = title,
                                    Track = track
                                }
                            };

                            var fileTypeSplit = file.GetType().FullName.Split('.');
                            if (fileTypeSplit.Length > 0)
                            {
                                switch (fileTypeSplit[1].ToLower())
                                {
                                    case "png":
                                    case "jpg":
                                    case "jpeg":
                                    case "bmp":
                                    case "image":
                                    playlistItemFile.Type = PlayListItemType.Image;
                                    break;
                                }
                            }

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
                }

                playlistItemFile = TryMakeFileTag(filePath, source);
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

        private PlaylistItemFile TryMakeFileTag(string filePath, PlayListItemSource source)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".pdf":
                    return new PdfService().GetTag(filePath);
                default:
                    if (source == PlayListItemSource.Streamed)
                    {
                        return new StreamService().GetTag(filePath);
                    }
                    break;
            }
            return null;
        }
    }
}
