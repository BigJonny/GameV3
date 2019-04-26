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

        /// <summary>
        /// Erstellt ein neues GameControl ohne spezifische Informationen.
        /// </summary>
        public GameControl()
        {
            font = BitmapFontManager.GetFontFromName("Calibri");
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
        }


        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            
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
