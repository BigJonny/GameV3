using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{
    /// <summary>
    /// Ein Klasse, die diverse Hilfsfunktionen zur Bearbeitung von Bilder leifert.
    /// </summary>
    public static class ImageHelper
    {


        /// <summary>
        /// Gibt die Koordinaten der Maus in Pixeln auf dem übergebenen Screen-Bild an.
        /// </summary>
        /// <param name="mousePos"></param>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static Point TranslateMousePos(Point mousePos, Bitmap screen, Point screenSize)
        {
            if (screen == null) return mousePos;
            else
            {
                if(screen.Height == 0 || screen.Width == 0)
                {
                    return mousePos;
                }
                else
                {
                    float ratioWidth = (float)screen.Width / screenSize.X;
                    float ratioHeight = (float)screen.Height / screenSize.Y;
                    // Scale the points by our ratio
                    float newX = mousePos.X;
                    float newY = mousePos.Y;
                    newX *= ratioWidth;
                    newY *= ratioHeight;
                    return new Point((int)newX, (int)newY);
                }
            }
        }

        /// <summary>
        /// Translatiert eine Größe in einen Punkt.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point ToPoint(this Size size)
        {
            return new Point(size.Width, size.Height);
        }


        public static Point Center(this Rectangle rec)
        {
            return new Point((rec.Width / 2) + rec.X, (rec.Height / 2) + rec.Y);
        }

    }
}
