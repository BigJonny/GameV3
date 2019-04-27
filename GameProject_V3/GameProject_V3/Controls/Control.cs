using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{

    /// <summary>
    /// Ein EventHandler zum Zeichnen von Elementen des Spiels.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void DrawEventHandler(object sender, DrawEventArgs args);

    /// <summary>
    /// Ein Steuerelement des Spiels.
    /// </summary>
    public abstract class Control : GameComponent
    {

        #region Varaiblen:
        private Color backColor;
        private Bitmap backgroundImage;
        private bool eneabled;
        private bool visible;

        private bool canFocus;
        private bool hasFocus;
        private Container parrent;

        private bool drawBorder;
        private Padding padding;

        private static object parrentChangedKey = new object();
        private static object visibleChangedKey = new object();
        private static object backColorChangedKey = new object();
        private static object eneabedChangedKey = new object();
        private static object afterPaintKey = new object();
        private static object beforPaintKey = new object();
        private static object focusChangedKey = new object();
        #endregion

        /// <summary>
        /// Erstellt ein neues Steuerelement.
        /// </summary>
        public Control()
        {
            size = new Point(200, 50);
            backColor = Color.White;
            visible = true;
            eneabled = true;
            backgroundImage = null;
            canFocus = false;
            hasFocus = false;
            drawBorder = true;
            padding = new Padding();
        }

        #region Drawing:
        public override void Draw(Graphics graphics, GameTime gameTime)
        {
            if(visible == true)
            {

                OnBeforPaint(this, new DrawEventArgs(graphics, gameTime));
                DrawControl(graphics, gameTime);
                OnAfterPaint(this, new DrawEventArgs(graphics, gameTime));
            }
        }

        /// <summary>
        /// Zeichnet das Steuerelement.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        protected abstract void DrawControl(Graphics graphics, GameTime gameTime);
        #endregion

        #region Update:
        public override void Update(GameTime gameTime)
        {
            if(eneabled == true)
            {
                UpdateMouse(gameTime);
                if(HasFocus)
                {
                    UpdateKeyInput(gameTime);
                }
                UpdateControl(gameTime);
            }
        }

        /// <summary>
        /// Updatet das Verhalten des Steuerlements in Bezug auf die Maus.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void UpdateMouse(GameTime gameTime);

        /// <summary>
        /// Updatet das Steruelement.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void UpdateControl(GameTime gameTime);

        /// <summary>
        /// Aktualisiert den Input der Tastatur in Bezug auf dieses Steuerelement.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void UpdateKeyInput(GameTime gameTime);
        #endregion

        #region Hilfsfuntkionen:
        /// <summary>
        /// Gibt true zurück, wenn der übergebene Punkt in Spiel-Koordinaten innerhalb der 
        /// Dimensionen des Steuerelements ist.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsPointInside(Point p)
        {
            return IsPointInside(p, new Rectangle(GetScreenLocation(), new Size(Size.X, Size.Y)));
        }

        /// <summary>
        /// Gibt true zurück, wenn der übergebene Punkt innerhalb des Rechtecks ist.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="rec"></param>
        /// <returns></returns>
        public bool IsPointInside(Point p, Rectangle rec)
        {
            if (p.X >= rec.X && p.X <= rec.X + rec.Width)
            {
                if (p.Y >= rec.Y && p.Y <= rec.Y + rec.Height)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gibt die Position dieses Steuerlements auf dem Bildschirm zurück.
        /// </summary>
        /// <returns></returns>
        public Point GetScreenLocation()
        {
            Point result = new Point(Location.X, Location.Y);
            if (Parrent != null)
            {
                result.X += Parrent.Location.X;
                result.Y += Parrent.Location.Y;
                return GetScreenLocation(result, Parrent);
            }
            return result;
        }

        /// <summary>
        /// rekursive Hilfsfunktion zur Ermittlung der Bildschrimposition.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        private Point GetScreenLocation(Point p, GameControl control)
        {
            if (control.Parrent != null)
            {
                p.X += control.Parrent.Location.X;
                p.Y += control.Parrent.Location.Y;
            }
            return p;
        }

        /// <summary>
        /// Überweißt diesem Steuerelement ein neues übergeordnetes Steuerlement.
        /// </summary>
        /// <param name="parrent"></param>
        public void SetParrent(Container parrent)
        {
            this.parrent = parrent;
            OnParrentChanged(this, EventArgs.Empty);
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn das Steuerelement die Sichtbarkeit ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnVisibleChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[visibleChangedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn der Aktivierungsstatus des Steuerelements verändert wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnEneneabledChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[eneabedChangedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich die Hintergrundfarbde des Steuerelements ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnBackColorChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[backColorChangedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, bevor das Steuerelement gezeichnet wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnBeforPaint(object sender, DrawEventArgs args)
        {
            DrawEventHandler handler;
            handler = (DrawEventHandler)Events[beforPaintKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, nachdem das Steuerlement gezeichnet wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnAfterPaint(object sender, DrawEventArgs args)
        {
            DrawEventHandler handler;
            handler = (DrawEventHandler)Events[afterPaintKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich der Fokus des Steuerlements ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnFocusChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[focusChangedKey];
            handler?.Invoke(sender, args);
        }


        /// <summary>
        /// Tritt ein, wenn sich der übergeordnete Container verändert hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnParrentChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[parrentChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt die Sichtbarkeit des Steuerelement zurück oder überschreibt diese.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if(value != visible)
                {
                    visible = value;
                    OnVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gibt den Aktivierungsstatus des Steuerlements zurück oder überschreibt diesen.
        /// </summary>
        public bool Eneabled
        {
            get
            {
                return eneabled;
            }
            set
            {
                if(value != eneabled)
                {
                    eneabled = value;
                    OnEneneabledChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gibt die Hintergrundfarbe des Steuerelements zurück.
        /// </summary>
        public Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                if(value != backColor)
                {
                    backColor = value;
                    OnBackColorChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gibt die Dimensionen des Steuerelements zurück.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Location.X, Location.Y, Size.X, Size.Y);
            }
        }

        /// <summary>
        /// Gibt an, ob um das Steuerelement ein schwarzes Rechteck gezeichnet werden soll.
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
        /// Gibt an, ob das Steuerlement den FOkus besitzt.
        /// </summary>
        public bool HasFocus
        {
            get
            {
                return hasFocus;
            }
            set
            {
                if(CanFocus)
                {
                    if(value != hasFocus)
                    {
                        hasFocus = value;
                        OnFocusChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Gibt an, ob das Steuerelement den einen Fokus haben kann.
        /// </summary>
        public bool CanFocus
        {
            get
            {
                return canFocus;
            }
            set
            {
                canFocus = value;
            }
        }

        /// <summary>
        /// Gibt das Hintergrundbild des Steuerlements zurück oder überschreibt dieses.
        /// </summary>
        public Bitmap BackgroundImage
        {
            get
            {
                return backgroundImage;
            }
            set
            {
                backgroundImage = value;
            }
        }

        /// <summary>
        /// Gibt das übergeordnete Steuerlement zurück.
        /// </summary>
        public Container Parrent
        {
            get
            {
                return parrent;
            }
        }

        /// <summary>
        /// Gibt das Padding dieses Steuerelement zurück oder überschreibt dieses.
        /// </summary>
        public Padding Padding
        {
            get
            {
                return padding;
            }
            set
            {
                if(value != null)
                {
                    padding = value;
                }
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich <see cref="Parrent"/> geändert hat.
        /// </summary>
        public event EventHandler ParrentChanged
        {
            add
            {
                Events.AddHandler(parrentChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(parrentChangedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich die Sichtbarkeit des Steuerlements verändert hat.
        /// </summary>
        public event EventHandler VisibleChanged
        {
            add
            {
                Events.AddHandler(visibleChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(visibleChangedKey, value);
            }
        }

        /// <summary>
        /// Tritt ein, wenn sich der Aktivirungsstatus des Steuerelements geändert hat.
        /// </summary>
        public event EventHandler EneabledChanged
        {
            add
            {
                Events.AddHandler(eneabedChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(eneabedChangedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich die Hintergrundfarbe des Steuelements geändert hat.
        /// </summary>
        public event EventHandler BackColorChanged
        {
            add
            {
                Events.AddHandler(backColorChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(backColorChangedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, bevor das Steuerlement gezeichnet wurde.
        /// </summary>
        public event DrawEventHandler BeforPaint
        {
            add
            {
                Events.AddHandler(beforPaintKey, value);
            }
            remove
            {
                Events.RemoveHandler(beforPaintKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, nachdem das Steuerlement gezeichnet wurde.
        /// </summary>
        public event DrawEventHandler AfterPaint
        {
            add
            {
                Events.AddHandler(afterPaintKey, value);
            }
            remove
            {
                Events.RemoveHandler(afterPaintKey, value);
            }
        }
        #endregion

    }
}
