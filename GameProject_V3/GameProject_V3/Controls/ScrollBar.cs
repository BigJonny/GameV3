using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Die verschiedenen Typen an ScrollBars.
    /// </summary>
    public enum ScrollType { Vertical = 0, Horizontal = 1 }

    /// <summary>
    /// Eine Liste die alle Richtungen beinhaltet in die die ScrollBar verschoben werden kann.
    /// </summary>
    public enum ScrollDirection
    {
        Up = 0, Down = 1, None = 2,
    }

    /// <summary>
    /// Ein Steuerlement welches einen Bildschrimverlauf ermöglicht.
    /// </summary>
    public class ScrollBar : KeyEventControl
    {

        //TODO: ScrollButtons implementieren und Scrollbar demenspechend anpassen.


        #region Variablen:
        private int value;
        private int maximum;
        private ScrollType type;
        private bool canMove = false;
        private Rectangle barBounds = new Rectangle();
        private Rectangle screenBarBounds = new Rectangle();
        private int scrollBarLength = 0;
        private ScrollDirection lastDir = ScrollDirection.None;

        private Bitmap drawArea;

        private MouseState oldState;
        private MouseState state;

        private static object valueChangeKey = new object();
        private static object maximumChangeKey = new object();
        private static object scrollImageChangeKey = new object();
        #endregion

        #region Konstruktoren:
        /// <summary>
        /// Erstellt eine neue ScrollBar mit einem bestimmten Typ.
        /// </summary>
        /// <param name="type"></param>
        public ScrollBar(ScrollType type)
        {
            value = 0;
            maximum = 100;
            scrollBarLength = 25;
            this.type = type;
            if (type == ScrollType.Vertical)
            {
                Width = 25;
            }
            else
            {
                Height = 25;
            }
            oldState = MouseInput.GetState();
        }
        #endregion

        #region Overrides:

        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            if (Parrent != null)
            {
                if (Scroll == ScrollType.Vertical)
                {
                    DrawVertical(graphics);
                }
                else
                {
                    DrawHorizontal(graphics);
                }
            }
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            if (Parrent != null)
            {
                if (Scroll == ScrollType.Vertical)
                {
                    UpdateVertical();
                }
                else
                {
                    UpdateHorizontal();
                }
            }
        }

        protected override void OnParrentChanged(object sender, EventArgs args)
        {
            base.OnParrentChanged(sender, args);
            if (Scroll == ScrollType.Vertical)
            {
                Location = new Point(Parrent.Width - this.Width, 0);
                Size = new Point(Width, Parrent.Height);
                float percent = GetPercentFromValue() * (Height - Width);
                barBounds = new Rectangle(0, (int)percent, Width, Width);
            }
            else
            {
                Location = new Point(0, Parrent.Height - Height);
                Size = new Point(Parrent.Width, Height);
                float percent = (GetPercentFromValue() * (Width - Height));
                barBounds = new Rectangle((int)percent, 0, Height, Height);
            }
        }
        #endregion

        #region Hilfsfunktionen:
        /// <summary>
        /// Zeichnet die Vertikale-ScrollBar.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawVertical(Graphics graphics)
        {
            drawArea = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(drawArea);
            g.Clear(BackColor);

            Point[] up = new Point[3];
            up[0] = new Point(Width / 2, 0);
            up[1] = new Point(0, 20);
            up[2] = new Point(Width, 20);

            Point[] down = new Point[3];
            down[0] = new Point(Width / 2, Height);
            down[1] = new Point(Width, Height - 20);
            down[2] = new Point(0, Height - 20);

            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, 0, Width, 20));
            g.FillPolygon(new SolidBrush(Color.Black), up);
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, 20));

            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, Height - 20, Width, 20));
            g.FillPolygon(new SolidBrush(Color.Black), down);
            g.DrawRectangle(Pens.Black, new Rectangle(0, Height - 20, Width, 20));

            g.FillRectangle(new SolidBrush(Color.Gray), barBounds);
            g.DrawRectangle(Pens.Black, new Rectangle(barBounds.X, barBounds.Y, barBounds.Width - 1, barBounds.Height - 1));

            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            graphics.DrawImage(drawArea, Bounds);
        }

        /// <summary>
        /// Zeichnet die Horizontale-ScrollBar.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawHorizontal(Graphics graphics)
        {
            drawArea = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(drawArea);
            g.Clear(BackColor);

            Point[] left = new Point[3];
            left[0] = new Point(0, Height / 2);
            left[1] = new Point(20, Height);
            left[2] = new Point(20, 0);

            Point[] right = new Point[3];
            right[0] = new Point(Width, Height / 2);
            right[1] = new Point(Width - 20, Height);
            right[2] = new Point(Width - 20, 0);

            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, 0, 20, Height));
            g.FillPolygon(new SolidBrush(Color.Black), left);
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 20, Height - 1));

            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Width - 20, 0, 20, Height));
            g.FillPolygon(new SolidBrush(Color.Black), right);
            g.DrawRectangle(Pens.Black, new Rectangle(Width - 20, 0, 20, Height));

            g.FillRectangle(new SolidBrush(Color.Gray), barBounds);
            g.DrawRectangle(Pens.Black, new Rectangle(barBounds.X, barBounds.Y, barBounds.Width - 1, barBounds.Height - 1));

            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            graphics.DrawImage(drawArea, Bounds);
        }

        /// <summary>
        /// Updatet die Vertikale Logik der ScrollBar.
        /// </summary>
        private void UpdateVertical()
        {
            state = MouseInput.GetState();
            screenBarBounds.Size = barBounds.Size;
            screenBarBounds.X = GetScreenLocation().X + barBounds.X;
            screenBarBounds.Y = GetScreenLocation().Y + barBounds.Y;
            if (IsPointInside(state.Position, screenBarBounds))
            {
                if (state.LeftButton == ButtonStates.Pressed && oldState.LeftButton == ButtonStates.Realesed)
                {
                    canMove = true;
                }
            }
            if (state.LeftButton == ButtonStates.Realesed)
            {
                canMove = false;
            }
            if (canMove)
            {
                if (state.Position.Y < screenBarBounds.Center().Y - 5)
                {
                    value--;
                    if (value < 0)
                    {
                        value = 0;
                    }
                    else
                    {
                        lastDir = ScrollDirection.Down;
                        ValueChange();
                        OnvalueChanged(this, EventArgs.Empty);
                    }
                }
                else if (state.Position.Y > screenBarBounds.Center().Y + 5)
                {
                    value++;
                    if (value > maximum)
                    {
                        value = maximum;
                    }
                    else
                    {
                        lastDir = ScrollDirection.Up;
                        ValueChange();
                        OnvalueChanged(this, EventArgs.Empty);
                    }
                }
            }
            oldState = state;
        }

        /// <summary>
        /// Updatet die horizontale Logik der ScrollBar.
        /// </summary>
        private void UpdateHorizontal()
        {
            state = MouseInput.GetState();
            screenBarBounds.Size = barBounds.Size;
            screenBarBounds.X = GetScreenLocation().X + barBounds.X;
            screenBarBounds.Y = GetScreenLocation().Y + barBounds.Y;
            if (IsPointInside(state.Position, screenBarBounds))
            {
                if (state.LeftButton == ButtonStates.Pressed && oldState.LeftButton == ButtonStates.Realesed)
                {
                    canMove = true;
                }
            }
            if (state.LeftButton == ButtonStates.Realesed)
            {
                canMove = false;
            }
            if (canMove)
            {
                if (state.Position.X < screenBarBounds.Center().X - 5)
                {
                    value--;
                    if (value < 0)
                    {
                        value = 0;
                    }
                    else
                    {
                        lastDir = ScrollDirection.Down;
                        ValueChange();
                        OnvalueChanged(this, EventArgs.Empty);
                    }
                }
                else if (state.Position.X > screenBarBounds.Center().X + 5)
                {
                    value++;
                    if (value > maximum)
                    {
                        value = maximum;
                    }
                    else
                    {
                        lastDir = ScrollDirection.Up;
                        ValueChange();
                        OnvalueChanged(this, EventArgs.Empty);
                    }
                }
            }
            oldState = state;
        }

        /// <summary>
        /// Gibt den prozentualen Anteil von <see cref="Value"/> von <see cref="Maximum"/> zurück.
        /// </summary>
        /// <returns></returns>
        private float GetPercentFromValue()
        {
            return ((float)value / (float)maximum);
        }

        /// <summary>
        /// Tritt ein, wenn sich der <seealso cref="Value"/> wert ändert
        /// </summary>
        private void ValueChange()
        {
            if (Scroll == ScrollType.Vertical)
            {
                float percent = GetPercentFromValue() * (Height - scrollBarLength);
                barBounds = new Rectangle(0, (int)percent, Width, scrollBarLength);
            }
            else
            {
                float percent = (GetPercentFromValue() * (Width - scrollBarLength));
                barBounds = new Rectangle((int)percent, 0, scrollBarLength, Height);
            }
        }

        /// <summary>
        /// Tritt ein, wenn sich der Wert <seealso cref="Maximum"/> ändert.
        /// </summary>
        private void MaximumChange()
        {
            if (Scroll == ScrollType.Vertical)
            {
                if (Height - maximum < 25)
                {
                    scrollBarLength = 25;
                }
                else
                {
                    scrollBarLength = Height - maximum;
                }
            }
            else
            {
                if (Width - maximum < 25)
                {
                    scrollBarLength = 25;
                }
                else
                {
                    scrollBarLength = Width - maximum;
                }
            }
            ValueChange();
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn sich der <see cref="Value"/>-Wert ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnvalueChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[valueChangeKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich der <see cref="Maximum"/>-Wert ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMaximumChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[maximumChangeKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt den aktuellen Scroll-Wert an oder überschreibt diesen.
        /// </summary>
        public int Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Gibt den maximalen Wert an den <see cref="Value"/> anehmen kann zurück
        /// oder überschreibt diesen.
        /// </summary>
        public int Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                maximum = value;
                if (maximum < 0)
                {
                    maximum = 0;
                }
                if (this.value > maximum)
                {
                    value = maximum;
                    ValueChange();
                    lastDir = ScrollDirection.Down;
                    ValueChange();
                    OnvalueChanged(this, EventArgs.Empty);
                }
                MaximumChange();
                OnMaximumChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gibt den aktuellen <see cref="ScrollType"/> dieser ScrollBar an.
        /// </summary>
        public ScrollType Scroll
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Gibt an, ob sich die ScrollBar bewegen kann.
        /// </summary>
        internal bool CanMove
        {
            get
            {
                return canMove;
            }
        }

        /// <summary>
        /// Gibt die letzte Richtung an in der die Scrollbar bewegt wurde.
        /// </summary>
        public ScrollDirection LastDirection
        {
            get
            {
                return lastDir;
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der <see cref="Value"/>-Wert ändert.
        /// </summary>
        public event EventHandler ValueChanged
        {
            add
            {
                Events.AddHandler(valueChangeKey, value);
            }
            remove
            {
                Events.RemoveHandler(valueChangeKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der <see cref="Maximum"/>-Wert ändert.
        /// </summary>
        public event EventHandler MaximumChanged
        {
            add
            {
                Events.AddHandler(maximumChangeKey, value);
            }
            remove
            {
                Events.RemoveHandler(maximumChangeKey, value);
            }
        }
        #endregion
    }
}

