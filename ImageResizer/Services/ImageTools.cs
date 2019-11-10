﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageResizer.Services
{
    public class ImageTools
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// https://stackoverflow.com/a/24199315
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="madWidth">The width to resize to.</param>
        /// <param name="maxHeight">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int madWidth, int maxHeight)
        {
            Decimal ratio = (Decimal)image.Width / (Decimal)image.Height;

            if(ratio > 1)
            {
                maxHeight = (int)(maxHeight / ratio);
            }
            if(ratio < 1)
            {
                madWidth = (int)(madWidth / ratio);
            }

            var destRect = new Rectangle(0, 0, madWidth, maxHeight);
            var destImage = new Bitmap(madWidth, maxHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
