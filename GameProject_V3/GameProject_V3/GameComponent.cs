using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{
    /// <summary>
    /// Stellt eine neue darstellbare Game-Komponente dar.
    /// </summary>
    public abstract class GameComponent
    {

        private Point location;
        private Point size;
        protected EventHandlerList Events;

        private static object locationChangedKey = new object();
        private static object sizeChangedKey = new object();

        /// <summary>
        /// Erstellt eine neue darstellbare Spielkomponente.
        /// </summary>
        public GameComponent()
        {
            Events = new EventHandlerList();
            size = new Point(0, 0);
            location = new Point(0, 0);
        }


        /// <summary>
        /// Updatet die Komponente.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Zeichnet die Komponente.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public abstract void Draw(Graphics graphics, GameTime gameTime);


        #region Events:
        /// <summary>
        /// Tritt ein, wenn sich die Position der Komponente ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnLocationChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[locationChangedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich die Größe der Komponente ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnSizeChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[sizeChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt die Breite der Komopente zurück oder überschreibt diese.
        /// </summary>
        public int Width
        {
            get
            {
                return size.X;
            }
            set
            {
                if(value != size.X)
                {
                    size = new Point(value, size.Y);
                    OnSizeChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gibt die Höhe der Komponente zurück oder überschreibt dieses.
        /// </summary>
        public int Height
        {
            get
            {
                return size.Y;
            }
            set
            {
                if(value != size.Y)
                {
                    size = new Point(size.X, value);
                    OnSizeChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gibt die Position der Komponte zurück oder überschreibt diese.
        /// </summary>
        public Point Location
        {
            get
            {
                return location;
            }
            set
            {
                if(value != location)
                {
                    location = value;
                    OnLocationChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gibt die Größe der Komponente zurück oder überschreibt diese.
        /// </summary>
        public Point Size
        {
            get
            {
                return size;
            }
            set
            {
                if(value != size)
                {
                    size = value;
                    OnSizeChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Ein Ereignis, welches ausgelöst wird, wenn die Komponente die Größe verändert.
        /// </summary>
        public event EventHandler SizeChanged
        {
            add
            {
                Events.AddHandler(sizeChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(sizeChangedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches ausgelöst wird, wenn die Komponente die Position verändert.
        /// </summary>
        public event EventHandler LocationChanged
        {
            add
            {
                Events.AddHandler(locationChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(locationChangedKey, value);
            }
        }
        #endregion
    }
}
