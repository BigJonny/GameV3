using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{
    public static class BitmapFontManager
    {

        private static string contentPath;
        private const string suffix = @"\Content\Fonts";
        private static List<BitmapFont> fonts = new List<BitmapFont>();
        private const string seperator = "----------------------------------------------------------------------------";

        /// <summary>
        /// Erstellt den Pfad aus dem alle Schriftarten geladen werden sollen. Falls 
        /// dieser nicht vorhanden ist wird eine Exception geworfen.
        /// </summary>
        private static void GenerateContentPath()
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + suffix) == false)
            {
                throw new DirectoryNotFoundException("BitmapFontManager: Konnte den Content" +
                    "-Ordner zum Laden aller Fonts nicht finden: " +
                    Directory.GetCurrentDirectory() + suffix);
            }
            else
            {
                contentPath = Directory.GetCurrentDirectory() + suffix;
            }
        }

        /// <summary>
        /// Lädt alle Schriftarten, die vorhanden sind.
        /// </summary>
        public static void LoadFonts()
        {
            GenerateContentPath();
            Console.WriteLine("BitmapFontManager: Lade Schriftarten...");
            Console.WriteLine(seperator);
            foreach (string directory in Directory.GetDirectories(contentPath))
            {
                Console.WriteLine("Versuche neue Schriftart aus folgendem Pfad zu laden: " + directory);
                BitmapFont font = new BitmapFont(directory);
                fonts.Add(font);
                Console.WriteLine("Schriftart '" + font.Name + "' wurde erfolgreich geladen");
            }
            Console.WriteLine(seperator);
        }

        /// <summary>
        /// Gibt die entsprechende zu dem übergebenen Namen gehörende BitmapFont zurück.
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static BitmapFont GetFontFromName(string fontName)
        {
            foreach(BitmapFont font in fonts)
            {
                if(fontName == font.Name)
                {
                    return font.Clone();
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt die Namen aller gespeicherten Schriftarten zurück.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFontNames()
        {
            List<string> result = new List<string>();
            foreach(BitmapFont font in fonts)
            {
                result.Add(font.Name);
            }
            return result;
        }
    }
}
