using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Stellt ein einfaches Label dar, welches Text auf dem Bildschirm anzeigt.
    /// </summary>
    public class Label : GameControl
    {
        #region Varaiblen:
        private string text;
        private bool drawBorder;
        private Bitmap drawArea;
        private bool drawBackgroundColor;
        private bool growWidthText;

        private static object textChangedKey = new object();
        #endregion

        /// <summary>
        /// Erstellt ein neues leeres Label.
        /// </summary>
        public Label() : base()
        {
            text = "";
            size = new Point(100, 50);
            drawArea = new Bitmap(Width, Height);
            growWidthText = true;
            drawBorder = false;
            drawBackgroundColor = false;
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            ReDraw();
            graphics.DrawImage(drawArea, Bounds);
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            ReDraw();
        }
        #endregion

        #region Hilfsfuntkeionen:
        /// <summary>
        /// Verändert die Größe des Steuerlements entsprechende der <see cref="Text"/>-Eigenschaft.
        /// </summary>
        private void TextSizeChanged()
        {
            Point textSize = Font.MeasureString(text);
            Width = textSize.X;
            Height = textSize.Y;
        }

        /// <summary>
        /// Zeichnet das Steuerelement neu.
        /// </summary>
        private void ReDraw()
        {
            drawArea = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(drawArea);
            if (drawBackgroundColor == true)
            {
                graphics.Clear(BackColor);
            }
            if (drawBorder == true)
            {
                graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(0, 0, Width - 1, Height - 1));
            }
            Font?.DrawString(text, graphics, new Point(0, 0));
            graphics.Dispose();
        }
        #endregion

        #region Evetns:
        /// <summary>
        /// Tritt ein, wenn sich der Text des Labels ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnTextChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[textChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt an, ob um das Label eine schwarzes Rechteck gezeichnet werden soll.
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
                ReDraw();
            }
        }

        /// <summary>
        /// Gibt an, ob die Hintergrundfarbe des Steuerlements eenfalls gezeichnet werden soll.
        /// </summary>
        public bool DrawBackgroundColor
        {
            get
            {
                return drawBackgroundColor;
            }
            set
            {
                drawBackgroundColor = value;
                ReDraw();
            }
        }

        /// <summary>
        /// Gibt an, ob die Größe des Labels mit der <see cref="Text"/>-Eigenschaft verändert werden kann.
        /// </summary>
        public bool GrowWidthText
        {
            get
            {
                return growWidthText;
            }
            set
            {
                growWidthText = value;
                ReDraw();
            }
        }

        /// <summary>
        /// Gibt den Text des Steuerelements zurück oder überschreibt diesen.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                if(growWidthText)
                {
                    TextSizeChanged();
                }
                OnTextChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches sich ändert, wenn sich die <see cref="Text"/>-Eigenschaft ändert.
        /// </summary>
        public event EventHandler TextChanged
        {
            add
            {
                Events.AddHandler(textChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(textChangedKey, value);
            }
        }
        #endregion
    }
}
