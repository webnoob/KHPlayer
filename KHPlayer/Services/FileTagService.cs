using System.IO;
using TagLib;
using File = TagLib.File;

namespace KHPlayer.Services
{
    public class FileTagService
    {
        public File GetTag(string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return File.Create(new StreamFileAbstraction(filePath, fileStream, fileStream));
                }
            }
            catch (IOException)
            {
                //If the file is in use (already playing) then we don't want to throw an error.
                //This will however prevent the same file being added to the play list when it's currently playing
                //I can live with that ...
                return null;
            }
        }
    }
}
