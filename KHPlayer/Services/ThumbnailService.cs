﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using KHPlayer.Classes;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public class ThumbnailService
    {
        private readonly FileTagService _fileTagService;

        public ThumbnailService()
        {
            _fileTagService = new FileTagService();
        }

        public string GenerateForFile(string filePath)
        {
            var thumbnailDirectory = String.Format("{0}\\{1}", PathHelper.GetApplicationPath(), Settings.Default.ThumbnailLocation);
            if (!Directory.Exists(thumbnailDirectory))
                Directory.CreateDirectory(thumbnailDirectory);

            var tagFile = _fileTagService.GetTag(filePath);
            if (tagFile == null)
                return "";

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
                if (string.IsNullOrEmpty(picture.FilePath))
                {
                    using (var ms = new MemoryStream(picture.Data.Data))
                    {
                        using (var returnImage = Image.FromStream(ms))
                        {
                            returnImage.Save(imageFilePath);
                        }
                    }
                }
                else
                    imageFilePath = picture.FilePath;

                return imageFilePath;
            }

            return "";
        }
    }
}
