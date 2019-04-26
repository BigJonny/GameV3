using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameProject_V3
{
    /// <summary>
    /// Eine Klasse, die der Font-Klasse ähnelt nur, dass die Bilder der 
    /// einzelnen Chars aus Bitmaps und nicht aus Texture2Ds bestehen.
    /// </summary>
    public class BitmapFont
    {
        #region Variable:
        private string name;
        private string path;
        private Tuple<Bitmap, int>[] chars;
        private int charCount;
        private Bitmap source;
        private Color color;

        private Regex nameRegex;
        private Regex charCountRegex;
        #endregion

        /// <summary>
        /// Erstellt eine neue BitmapFont aus dem gegeben Pfad.
        /// </summary>
        /// <param name="path"></param>
        public BitmapFont(string path)
        {
            this.path = path;
            nameRegex = new Regex("^name = {1,}[a-zA-Z]");
            charCountRegex = new Regex("^chars count={1,}[0-9]");
            Parse();
            FontColor = Color.Black;
        }

        #region Hilfsmethoden:
        /// <summary>
        /// Versucht die Schriftart aus einem gegebenen Pfad zu laden.
        /// </summary>
        private void Parse()
        {
            if (File.Exists(path + "/source.png"))
            {
                source = new Bitmap(path + "/source.png");
            }
            else
            {
                throw new FileNotFoundException("Konnte Source-File nicht finden. "
                    + path + @"\source.png existiert nicht!");
            }
            if (File.Exists(path + "/font.fnt"))
            {
                StreamReader reader = new StreamReader(path + "/font.fnt");
                string line = reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                string nameLine = reader.ReadLine();
                if (nameRegex.IsMatch(nameLine))
                {
                    name = GetValueFromLine(nameLine);
                }
                else
                {
                    throw new Exception("Der Name der Schriftart konnte nicht dekodiert werden.");
                }
                string charCountLine = reader.ReadLine();
                if (charCountRegex.IsMatch(charCountLine))
                {
                    charCountLine = GetValueFromLine(charCountLine);
                    charCount = Convert.ToInt32(charCountLine);
                    chars = new Tuple<Bitmap, int>[charCount];
                }
                else
                {
                    throw new Exception("Die absolute Anzahl an Zeichen der Schriftart " +
                        name + " konnte nicht geladen werden.");
                }
                for (int i = 0; i < charCount; i++)
                {
                    line = reader.ReadLine();
                    line = FormatLine(line);
                    line = line.Replace(" ", string.Empty);
                    int charIndex = Convert.ToInt16(line.Substring(0, line.IndexOf(",")));
                    line = line.Substring(line.IndexOf(",") + 1);
                    int x = Convert.ToInt16(line.Substring(0, line.IndexOf(",")));
                    line = line.Substring(line.IndexOf(",") + 1);
                    int y = Convert.ToInt16(line.Substring(0, line.IndexOf(",")));
                    line = line.Substring(line.IndexOf(",") + 1);
                    int width = Convert.ToInt32(line.Substring(0, line.IndexOf(",")));
                    line = line.Substring(line.IndexOf(",") + 1);
                    int height = Convert.ToInt32(line.Substring(0, line.IndexOf(",")));

                    Bitmap charTex = GetPotion(new Rectangle(x, y, width, height));
                    Tuple<Bitmap, int> tuple = new Tuple<Bitmap, int>(charTex, charIndex);
                    chars[i] = tuple;
                }
                reader.Close();
            }
            else
            {
                throw new FileNotFoundException("Konnte BitFont-File nicht finden. "
                    + path + @"\font.fnt existiert nicht!");
            }
        }

        /// <summary>
        /// Gibt einen Teil des übergbenen Bildes zurück, welches dem 
        /// übergebenen Rechteck entspricht.
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="destRec"></param>
        /// <returns></returns>
        private Bitmap GetPotion(Rectangle destRec)
        {
            PixelFormat format = source.PixelFormat;
            Bitmap result = source.Clone(destRec, format);
            return result;
        }

        /// <summary>
        /// Gibt den inhalt der übergebenen Zeichfolge zurück der sich hinter dem 
        /// Gleicheitszeichen befindet.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string GetValueFromLine(string line)
        {
            line = line.Replace(" ", string.Empty);
            int index = line.IndexOf('=') + 1;
            return line.Substring(index, line.Length - index);
        }

        /// <summary>
        /// Formatiert die übergebene Zeichkette so, dass jedes Auftauchen 
        /// eines der Worte : ("x=", "y=", "width=", "height=",
        /// "char id=", "xoffset=") durch ein Komma ersetzt werden.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string FormatLine(string line)
        {
            string result = line.Replace("x=", ",");
            result = result.Replace("y=", ",");
            result = result.Replace("width=", ",");
            result = result.Replace("height=", ",");
            result = result.Replace("char id=", string.Empty);
            result = result.Replace("xoffset=", ",");
            return result;
        }

        /// <summary>
        /// Gibt das Bild des Characters zurück, welches dem 
        /// übergebenen Ascii-Code entspricht.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Bitmap GetCharFromIndex(int index)
        {
            foreach(Tuple<Bitmap, int> tuple in chars)
            {
                if(tuple.Item2 == index)
                {
                    return tuple.Item1;
                }
            }
            return null;
        }

        /// <summary>
        /// Verändert jedes auftauchen von oldColor im Bild image zu newColor.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        private void ReColorImage(Bitmap image, Color newColor)
        {
            for(int x = 0; x < image.Width; x++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    if(image.GetPixel(x, y).A != 0)
                    {
                        image.SetPixel(x, y, newColor);
                    }
                }
            }
        }

        /// <summary>
        /// Gibt eine Kopie dieser Schriftart zurück.
        /// </summary>
        /// <returns></returns>
        public BitmapFont Clone()
        {
            BitmapFont result = new BitmapFont(path);
            return result;
        }

        /// <summary>
        /// Misst Höhe und Breite des übergebenen Strings als BitmapFont.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Point MeasureString(string text)
        {
            if(text != null)
            {
                Point result = new Point(0, 0);
                if(text.Length > 0 && GetCharFromIndex(text[0]) != null)
                {
                    result.Y = GetCharFromIndex(text[0]).Height;
                }
                foreach(char c in text)
                {
                    if(GetCharFromIndex(c) != null)
                    {
                        result.X += GetCharFromIndex(c).Width;
                    }
                }
                return result;
            }
            return new Point(0, 0);
        }

        /// <summary>
        /// Bemisst die Größe eines einzenlen Characters.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Point MeasureChar(char c)
        {
            Point result = new Point(0, 0);
            Bitmap charImage = GetCharFromIndex(c);
            if(charImage != null)
            {
                result.X = charImage.Width;
                result.Y = charImage.Height;
            }
            return result;
        }

        /// <summary>
        /// Gibt an, ob der übergebene <see cref="Char"/> in dieser SchriftArt enthalten ist.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool HasChar(char c)
        {
            foreach(Tuple<Bitmap, int> tuple in chars)
            {
                if(c == tuple.Item2)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Zeichnet den gegebenen String auf dem gegebenen Graphics-Objekt an der gegebenen
        /// Position.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="graphics"></param>
        /// <param name="position"></param>
        public void DrawString(string message, Graphics graphics, Point position)
        {
            foreach(char c in message)
            {
                Bitmap charImage = GetCharFromIndex(c);
                graphics.DrawImage(charImage, position);
                position.X += charImage.Width;
            }
        }

        /// <summary>
        /// Zeichnet den übergebenen String in dem gegebenen Rechteck und beachtet dabei die übergebene Textausrichtung.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="graphics"></param>
        /// <param name="drawArea"></param>
        /// <param name="alignment"></param>
        public void DrawString(string message, Graphics graphics, Rectangle drawArea, TextAlignment alignment)
        {
            Point position = drawArea.Location;
            Point textSize = MeasureString(message);

            if (alignment == TextAlignment.Center)
            {
                position.X += (drawArea.Width / 2) - textSize.X / 2;
                position.Y += (drawArea.Height / 2) - textSize.Y / 2;
            }
            else if (alignment == TextAlignment.Left)
            {
                position.Y += (drawArea.Height / 2) - textSize.Y / 2;
            }
            else if (alignment == TextAlignment.Right)
            {
                position.Y += (drawArea.Height / 2) - textSize.Y / 2;
                position.X += drawArea.Width - textSize.X;
            }
            else if (alignment == TextAlignment.Bottum)
            {
                position.X += (drawArea.Width / 2) - textSize.X / 2;
                position.Y += drawArea.Height - textSize.Y;
            }
            else if (alignment == TextAlignment.Top)
            {
                position.X += (drawArea.Width / 2) - textSize.X / 2;
            }
            DrawString(message, graphics, position);
        }

        #region Eigenschaften:
        /// <summary>
        /// Gibt den Namen der Schriftart zurück.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gibt den Pfad zurück aus dem diese Schriftart erstellt wurde.
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
        }

        /// <summary>
        /// Gibt die Farbe der Schriftart zurück oder überschreibt diese.
        /// </summary>
        public Color FontColor
        {
            get
            {
                return color;
            }
            set
            {
                foreach(Tuple<Bitmap, int>tuple in chars)
                {
                    ReColorImage(tuple.Item1, value);
                }
                color = value;
            }
        }

        /// <summary>
        /// Gibt eine Liste mit Tupeln zurück, die sowahl das Bild des Character als auch den dazugeörigen ASCII-Wert enthält.
        /// </summary>
        private Tuple<Bitmap, int>[] Chars
        {
            get
            {
                return chars;
            }
        }
        #endregion
    }
}
