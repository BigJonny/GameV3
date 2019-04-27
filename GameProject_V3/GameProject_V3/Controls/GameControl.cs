using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    public class GameControl : KeyEventControl
    {


        private BitmapFont font;
        private static object fontChangedKey = new object();

        protected Bitmap DrawArea;
        protected Graphics graphics;

        /// <summary>
        /// Erstellt ein neues GameControl ohne spezifische Informationen.
        /// </summary>
        public GameControl()
        {
            font = BitmapFontManager.GetFontFromName("Calibri");
            DrawArea = new Bitmap(size.X, size.Y);
        }

        /// <summary>
        /// Erstellt ein neues GameControl mit den gegebenen Dimensionen.
        /// </summary>
        /// <param name="bounds"></param>
        public GameControl(Rectangle bounds)
        {
            Location = bounds.Location;
            Size = bounds.Size.ToPoint();
            font = BitmapFontManager.GetFontFromName("Calibri");
            DrawArea = new Bitmap(bounds.Width, bounds.Height);
        }


        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            this.graphics = Graphics.FromImage(DrawArea);
            this.graphics.Clear(BackColor);
            if(BackgroundImage != null)
            {
                this.graphics.DrawImage(BackgroundImage, new Rectangle(0, 0, Width, Height));
            }
            if (DrawBorder)
            {
                this.graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }

        protected override void OnAfterPaint(object sender, DrawEventArgs args)
        {
            base.OnAfterPaint(sender, args);
            graphics.Dispose();
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            DrawArea.Dispose();
            DrawArea = new Bitmap(Width, Height);
        }

        #region Events:
        /// <summary>
        /// Tritt ein, wenn die aktuelle Schriftart geändert wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnFontChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[fontChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion


        /// <summary>
        /// Die aktuelle Schriftart des Steuerelements.
        /// </summary>
        public BitmapFont Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
                OnFontChanged(this, KeyEventArgs.Empty);
            }
        }


        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn die aktuelle Schriftart geändert wurtde.
        /// </summary>
        public event EventHandler FontChanged
        {
            add
            {
                Events.AddHandler(fontChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(fontChangedKey, value);
            }
        }

    }
}
