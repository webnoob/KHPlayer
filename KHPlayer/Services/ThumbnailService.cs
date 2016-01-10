using System;
using System.Drawing;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Properties;
using TagLib;
using File = System.IO.File;

namespace KHPlayer.Services
{
    public class ThumbnailService
    {
        public string GenerateForFile(string filePath)
        {
            var thumbnailDirectory = String.Format("{0}\\{1}", PathHelper.GetApplicationPath(), Settings.Default.ThumbnailLocation);
            if (!Directory.Exists(thumbnailDirectory))
                Directory.CreateDirectory(thumbnailDirectory);

            var f = new Ffmpeg();
            var finalImagePath = String.Format("{0}\\{1}.jpeg", thumbnailDirectory, Path.GetFileName(filePath));
            if (File.Exists(finalImagePath))
                return finalImagePath;

            var imageAdded = false;
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var tagFile = TagLib.File.Create(new StreamFileAbstraction(filePath, fileStream, fileStream));
                
                foreach (var picture in tagFile.Tag.Pictures)
                {
                    var imageFilePath =
                        String.Format("{0}\\{1}\\{2}.{3}",
                            PathHelper.GetApplicationPath(),
                            Settings.Default.ThumbnailLocation,
                            Path.GetFileName(filePath), picture.MimeType.Split('/').Last()
                            );
                    if (File.Exists(imageFilePath))
                        return imageFilePath;

                    using (var ms = new MemoryStream(picture.Data.Data))
                    {
                        using (var returnImage = Image.FromStream(ms))
                        {
                            returnImage.Save(imageFilePath);
                        }
                    }
                    imageAdded = true;
                    finalImagePath = imageFilePath;
                }
            }

            if (!imageAdded)
                f.GetThumbnail(filePath, finalImagePath, "120x120");

            return finalImagePath;
        }
    }
}
