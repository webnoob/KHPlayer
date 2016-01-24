using System.IO;
using System.Windows.Forms;
using KHPlayer.Extensions;

namespace KHPlayer.Helpers
{
    public static class PathHelper
    {
        public static string GetApplicationPath()
        {
            return Application.StartupPath;
        }

        public static bool FileIsLocked(string filePath)
        {
            var isStreamed = filePath.ToLower().Contains("http://") ||
                filePath.ToLower().Contains("https://") ||
                filePath.ToLower().Contains("ftp://");

            if (isStreamed)
                return false;

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.IsLocked())
                return true;

            return false;
        }
    }
}
