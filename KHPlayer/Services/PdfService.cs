using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using KHPlayer.Classes;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public class PdfService
    {
        public PlaylistItemFile GetTag(string filePath)
        {
            using (var reader = new PdfReader(filePath))
            {
                var title = reader.Info.ContainsKey("Title") && !string.IsNullOrEmpty(reader.Info["Title"])
                    ? reader.Info["Title"]
                    : Path.GetFileNameWithoutExtension(filePath);

                return new PlaylistItemFile
                {
                    FileName = filePath,
                    Type = PlayListItemType.Pdf,
                    Tag = new PlayListItemFileTag
                    {
                        Title = title,
                        Pictures = new List<PlayListItemFileTagImage>
                        {
                            new PlayListItemFileTagImage
                            {
                                MimeType = "image/png",
                                FilePath = PathHelper.GetApplicationPath() + "\\" + Settings.Default.ThumbnailLocation + "\\pdfimage.png"
                            }
                        }
                    }
                };
            }
        }
    }
}
