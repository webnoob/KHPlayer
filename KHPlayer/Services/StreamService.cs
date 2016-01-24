using System.Collections.Generic;
using System.IO;
using KHPlayer.Classes;
using KHPlayer.Helpers;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public class StreamService
    {
        public PlaylistItemFile GetTag(string url)
        {
            var fileExtension = Path.GetExtension(url);
            var fileName = Path.GetFileNameWithoutExtension(url);
            
            return new PlaylistItemFile
            {
                FileName = url,
                Type = fileExtension == ".mp3" ? PlayListItemType.Audio : PlayListItemType.Video,
                Tag = new PlayListItemFileTag
                {
                    Title = fileName,
                    Pictures = new List<PlayListItemFileTagImage>
                        {
                            new PlayListItemFileTagImage
                            {
                                MimeType = "image/png",
                                FilePath = PathHelper.GetApplicationPath() + "\\" + Settings.Default.ThumbnailLocation + "\\streamimage.png"
                            }
                        }
                }
            };
        }
    }
}