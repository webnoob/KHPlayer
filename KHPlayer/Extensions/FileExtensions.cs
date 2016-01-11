using System.IO;

namespace KHPlayer.Extensions
{
    public static class FileExtensions
    {
        /// <summary>
        /// Check if a file is in use. Note: Potential race condition when using this as it might not be locked when checking 
        /// but becomes locked immediately afterwards.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool IsLocked(this FileInfo f)
        {
            try
            {
                var fs = File.OpenWrite(f.FullName);
                fs.Close();
                return false;
            }
            catch (IOException) { return true; }
        }
    }
}
