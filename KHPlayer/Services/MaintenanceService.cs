using System;
using System.Collections.Generic;
using System.IO;
using KHPlayer.Classes;
using KHPlayer.Helpers;
using KHPlayer.Properties;

namespace KHPlayer.Services
{
    public enum MaintenanceIntegrityResultStatus
    {
        None, 
        Passed, 
        Failed
    }

    public class MaintenanceIntegrityResult
    {
        public string FilePath { get; set; }

        public MaintenanceIntegrityResultStatus Status { get; set; }

        public string StatusDetail { get; set; }
    }

    public class MaintenanceService
    {
        private readonly FileTagService _fileTagService;

        public MaintenanceService()
        {
            _fileTagService = new FileTagService();
        }

        public IEnumerable<MaintenanceIntegrityResult> CheckMediaFiles()
        {
            var locationsToCheck = new List<string>
            {
                PathHelper.GetApplicationPath(Settings.Default.SongLocation)
            };

            var results = new List<MaintenanceIntegrityResult>();
            foreach (var locationToCheck in locationsToCheck)
            {
                try
                {
                    var files = Directory.GetFiles(locationToCheck);
                    foreach (var file in files)
                    {
                        try
                        {
                            //First ensure we can get the tag for the file.,
                            var tag = _fileTagService.GetTag(file, PlayListItemSource.Disk);
                            
                            
                            results.Add(new MaintenanceIntegrityResult
                            {
                                FilePath = file,
                                Status = MaintenanceIntegrityResultStatus.Passed,
                                StatusDetail = tag.Tag.Title
                            });
                        }
                        catch (Exception e)
                        {
                            results.Add(new MaintenanceIntegrityResult
                            {
                                FilePath = file,
                                Status = MaintenanceIntegrityResultStatus.Failed,
                                StatusDetail = e.Message + Environment.NewLine + e.StackTrace
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    results.Add(new MaintenanceIntegrityResult
                    {
                        Status = MaintenanceIntegrityResultStatus.Failed,
                        StatusDetail = e.Message + Environment.NewLine + e.StackTrace
                    });
                }
            }

            return results;
        }
    }
}
