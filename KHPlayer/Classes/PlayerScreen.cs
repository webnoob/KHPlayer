using System.Drawing;

namespace KHPlayer.Classes
{
    public class PlayerScreen
    {
        public string Guid { get; set; }

        public string FriendlyName { get; set; }

        public ScreenDevice ScreenDevice { get; set; }

        public AudioDevice AudioDevice { get; set; }

        public bool DefaultScreen { get; set; }

        public string ColourName { get; set; }
    }

    public class ScreenDevice
    {
        public string DeviceName { get; set; }

        public int Id { get; set; }
    }
}
