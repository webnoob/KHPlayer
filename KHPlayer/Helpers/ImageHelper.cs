using System;
using System.Drawing;

namespace KHPlayer.Helpers
{
    public static class ImageHelper
    {
        public static Image ResizeImage(string file, int width, int height, bool onlyResizeIfWider)
        {
            using (var image = Image.FromFile(file))
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
}
