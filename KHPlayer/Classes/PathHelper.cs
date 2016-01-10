using System.IO;
using System.Windows.Forms;

namespace KHPlayer.Classes
{
    public static class PathHelper
    {
        public static string GetApplicationPath()
        {
            return Application.StartupPath;
        }
    }
}
