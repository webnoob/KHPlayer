using System;
using System.Drawing;
using System.IO;

namespace KHPlayer.Helpers
{
    public static class ImageHelper
    {
        public static Image ResizeImage(string file, int width, int height, bool onlyResizeIfWider)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
                return null;

            using (var image = Image.FromFile(file))
            {
                return ResizeImage(image, width, height, onlyResizeIfWider);
            }
        }

        public static Image ResizeImage(Image image, int width, int height, bool onlyResizeIfWider)
        {
            // Prevent using images internal thumbnail
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
                if (image.Width <= width)
                    width = image.Width;

            var newHeight = image.Height * width / image.Width;
            if (newHeight > height)
            {
                // Resize with height instead
                width = image.Width * height / image.Height;
                newHeight = height;
            }

            return image.GetThumbnailImage(width, newHeight, null, IntPtr.Zero);
        }
    }
}
