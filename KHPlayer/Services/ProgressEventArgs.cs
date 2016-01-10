using System;

namespace KHPlayer.Services
{
    public class ProgressEventArgs : EventArgs
    {
        public string Status { get; private set; }

        public int MaxItems { get; private set; }

        public int CurrentItem { get; private set; }

        public double CurrentItemPercentage { get; private set; }

        public ProgressEventArgs(string status, int currentItem, int maxItems, double currentItemPercentage)
        {
            Status = status;
            CurrentItem = currentItem;
            MaxItems = maxItems;
            CurrentItemPercentage = currentItemPercentage;
        }
    }
}