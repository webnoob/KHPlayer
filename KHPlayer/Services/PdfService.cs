using System;
using System.IO;
using iTextSharp.text.pdf;
using KHPlayer.Classes;

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
                        Title = title
                    }
                };
            }
        }
    }
}
