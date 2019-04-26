using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    public class DrawEventArgs : EventArgs
    {

        private Graphics graphics;
        private GameTime gameTime;

        /// <summary>
        /// Erstellt ein neues DrawEventArgs-Objekt.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public DrawEventArgs(Graphics graphics, GameTime gameTime)
        {
            this.graphics = graphics;
            this.gameTime = gameTime;
        }


        /// <summary>
        /// Gibt das Graphics-Objekt zurück, welches zum zeichnen verwendet wurde.
        /// </summary>
        public Graphics GraphicsDevice
        {
            get
            {
                return graphics;
            }
        }

        /// <summary>
        /// Gibt das aktuelle GameTime-Objekt zurück.
        /// </summary>
        public GameTime Time
        {
            get
            {
                return gameTime;
            }
        }


    }
}
