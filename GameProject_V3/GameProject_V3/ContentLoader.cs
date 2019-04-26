using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{
    /// <summary>
    /// Eine statische Klasse, den den SpielContent laden kann.
    /// </summary>
    public static class ContentLoader
    {

        private static string contentPath;

        /// <summary>
        /// Initialisiert den ConentLoader.
        /// </summary>
        public static void Initialize()
        {
            contentPath = Environment.CurrentDirectory + @"\Content\GameContent\";
        }

        /// <summary>
        /// Lädt ein Bild mit dem gegebenen Namen aus dem GameContent-Ordner.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Bitmap LoadImage(string name)
        {
            try
            {
                Bitmap result = (Bitmap)Image.FromFile(contentPath + name);
                return result;
            }
            catch
            {
                throw new FileNotFoundException("Konnte folgende Bild-Datei nicht finden: " + contentPath + name);
            }
        }


    }
}
