using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Ein Liste möglichen Modies zur Präsentation des Bildes der PictureBox.
    /// </summary>
    public enum SizeModes
    {
        StrechImage = 0, AutoSize = 1, Normal = 2,
    }

    /// <summary>
    /// Ein Klasse die eine simple PictureBox implementiert.
    /// </summary>
    public class PictureBox : GameControl
    {
        #region Varaiblen:
        private Bitmap image;
        private SizeModes sizemode;
        private Bitmap drawArea;
        private bool drawBorder;

        private static object imageChagendKey = new object();
        private static object sizeModeChangedKey = new object();
        #endregion

        //Erstellt eine neue leere Picturebox ohne Bild.
        public PictureBox() : base()
        {
            image = null;
            drawBorder = true;
            sizemode = SizeModes.Normal;
            Width = 100;
            Height = 100;
        }
        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            Graphics g = Graphics.FromImage(drawArea);
            g.Clear(BackColor);
            if(image != null)
            {
                if(sizemode == SizeModes.Normal || sizemode == SizeModes.AutoSize)
                {
                    g.DrawImage(image, new Point(0, 0));
                }
                else
                {
                    g.DrawImage(image, new Rectangle(0, 0, Width, Height));
                }
            }
            if(drawBorder)
            {
                g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(0, 0, Width - 1, Height - 1));
            }
            graphics.DrawImage(drawArea, Bounds);
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            drawArea = new Bitmap(Width, Height);
        }
        #endregion

        #region Evetns:
        /// <summary>
        /// Tritt ein, wenn sich das Bild der PictureBox geändert hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnImageChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[imageChagendKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich der SizeModes der PictureBox ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnSizeModeChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[sizeModeChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt das Bild der PictureBox zurück.
        /// </summary>
        public Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                if(sizemode == SizeModes.AutoSize && image != null)
                {
                    Width = image.Width;
                    Height = image.Height;
                }
                OnImageChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gibt an, ob die PictureBox von einem Schwarzen Rand umgebenen ist oder überschreibt dieses Eigenschaft.
        /// </summary>
        public bool DrawBorder
        {
            get
            {
                return drawBorder;
            }
            set
            {
                drawBorder = value;
            }
        }

        /// <summary>
        /// Der Präsentationsmodus der PictureBox.
        /// </summary>
        public SizeModes SizeMode
        {
            get
            {
                return sizemode;
            }
            set
            {
                if(value != sizemode)
                {
                    sizemode = value;
                    if(sizemode == SizeModes.AutoSize)
                    {
                        if(image != null)
                        {
                            Width = image.Width;
                            Height = image.Height;
                        }
                    }
                    OnSizeModeChanged(this, EventArgs.Empty);
                }
                sizemode = value;
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der Wert von <see cref="Image"/> geändert hat.
        /// </summary>
        public event EventHandler ImageChanged
        {
            add
            {
                Events.AddHandler(imageChagendKey, value);
            }
            remove
            {
                Events.RemoveHandler(imageChagendKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der Wert von <see cref="SizeMode"/> geändert hat.
        /// </summary>
        public event EventHandler SizeModeChanged
        {
            add
            {
                Events.AddHandler(sizeModeChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(sizeModeChangedKey, value);
            }
        }
        #endregion
    }
}
