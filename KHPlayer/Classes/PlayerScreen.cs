namespace KHPlayer.Classes
{
    public class PlayerScreen
    {
        public string Guid { get; set; }

        public string FriendlyName { get; set; }

        public ScreenDevice ScreenDevice { get; set; }

        public AudioDevice AudioDevice { get; set; }
    }

    public class ScreenDevice
    {
        public string DeviceName { get; set; }

        public int Id { get; set; }
    }
}
