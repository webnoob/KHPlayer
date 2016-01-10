using System;
using System.Diagnostics;

namespace KHPlayer.Classes
{
    public class Ffmpeg
    {
        private Process _ffmpeg;

        public void Exec(string input, string output, string parametri)
        {
            _ffmpeg = new Process
            {
                StartInfo =
                {
                    //Arguments = " -i " + input + (parametri != null ? " " + parametri : "") + " " + output,
                    Arguments = String.Format(" -i {0} -vframes 1 -ss 00:00:02.000 -y {1}", input, output),
                    FileName = String.Format("{0}\\Utils\\ffmpeg.exe", PathHelper.GetApplicationPath()),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            _ffmpeg.Start();
            _ffmpeg.WaitForExit(500);
            _ffmpeg.Close();
        }


        public void GetThumbnail(string video, string jpg, string velicina)
        {
            if (velicina == null) velicina = "120x120";
            Exec(video, jpg, "-s " + velicina);
        }
    }
}
