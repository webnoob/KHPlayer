using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KHPlayer.Classes;
using KHPlayer.Helpers;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public class PlaylistImportExportService
    {
        private readonly PlayListItemService _playListItemService;

        public delegate void ProgressUpdateHandler(object sender, ProgressEventArgs e);
        public event ProgressUpdateHandler OnUpdateProgress;

        public PlaylistImportExportService()
        {
            _playListItemService = new PlayListItemService();

            if (!Directory.Exists(Settings.Default.ImportLocation))
                Directory.CreateDirectory(Settings.Default.ImportLocation);
        }

        private void UpdateProgress(string status, int currentItem, int maxItems, double currentItemPercentage)
        {
            // Make sure someone is listening to event
            if (OnUpdateProgress == null) 
                return;

            var args = new ProgressEventArgs(status, currentItem, maxItems, currentItemPercentage);
            OnUpdateProgress(this, args);
        }

        public async Task ImportFromDirectory(string path, PlayList playList)
        {
            if (!Directory.Exists(path))
                return;

            var allFiles = GetFiles(path, new[] {"*.mp3", "*.mp4", "*.wma"}, SearchOption.AllDirectories).ToList();
            if (allFiles.Any())
                await ImportFiles(allFiles, playList);
        }

        public async Task ImportFiles(IEnumerable<String> filePaths, PlayList playList)
        {
            var files = filePaths.ToArray();
            var max = files.Count();
            
            for (var i = 0; i < max; i++)
            {
                var filePath = files[i];
                var currentItem = i + 1;
                UpdateProgress(String.Format("Importing - {0} / {1}", currentItem, max), currentItem, max, 0);

                var fileName = Path.GetFileName(filePath);
                var newFilePath = Settings.Default.ImportLocation + "\\" + fileName;

                if (!File.Exists(newFilePath))
                    await
                        FileEx.CopyAsync(filePath, newFilePath,
                            new Progress<double>(
                                p => UpdateProgress(String.Format("Copying {0}", newFilePath), currentItem, max, p)));
                
                _playListItemService.AddToPlaylistUsingFilePath(newFilePath, playList);
            }
        }

        public async Task ImportFiles(string filePath, PlayList playList)
        {
            await ImportFiles(new[] { filePath }, playList);
        }

        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel()
                   .SelectMany(searchPattern =>
                          Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
    }
}
