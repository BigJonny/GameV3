using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Ein Steuerelement, welches einen simplen Button implementiert.
    /// </summary>
    public class Button : GameControl
    {

        #region Variablen:
        private string text;
        private Bitmap mouseOverImage;
        private Bitmap mousePressImage;

        private TextAlignment alignment;
        bool mouseOver = false;
        bool mousePress = false;

        private static object textChangedKey = new object();
        #endregion

        #region Konstruktoren und Initialisierung:
        public Button()
        {
            text = "";
            alignment = TextAlignment.Center;
            InitImages();
        }

        /// <summary>
        /// Erstellt einenen neuen Button mit dem gegebenen Text.
        /// </summary>
        /// <param name="text"></param>
        public Button(string text)
        {
            this.text = text;
            alignment = TextAlignment.Center;
            InitImages();
        }

        /// <summary>
        /// erstellt einen neuen Button mit dem gegebenen Text und den gegebenen Dimensionen.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="bounds"></param>
        public Button(string text, Rectangle bounds) : base(bounds)
        {
            this.text = text;
            alignment = TextAlignment.Center;
            InitImages();
        }

        /// <summary>
        /// Initialisiert alle Bilder.
        /// </summary>
        private void InitImages()
        {
            if (mouseOverImage != null)
                mouseOverImage.Dispose();
            if (mousePressImage != null)
                mousePressImage.Dispose();

            mouseOverImage = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(mouseOverImage);
            g.Clear(Color.FromArgb(200, Color.Gray));
            g.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), new Rectangle(0, 0, Width - 1, Height - 1));

            mousePressImage = new Bitmap(Width, Height);
            Graphics g2 = Graphics.FromImage(mousePressImage);
            g2.Clear(Color.Gray);
            g2.DrawRectangle(new Pen(new SolidBrush(Color.Yellow)), new Rectangle(0, 0, Width - 1, Height - 1));
            g.Dispose();
            g2.Dispose();
        }
        #endregion

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            if(mouseOver)
            {
                if(mousePress)
                {
                    this.graphics.DrawImage(mousePressImage, new Rectangle(0, 0, Width, Height));
                }
                else
                {
                    this.graphics.DrawImage(mouseOverImage, new Rectangle(0, 0, Width, Height));
                }
            }
            Font.DrawString(text, this.graphics, new Rectangle(0, 0, Width, Height), Alignment);
            graphics.DrawImage(DrawArea, Bounds);
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
        }

        protected override void OnMouseEnter(object sender, MouseEventArgs args)
        {
            base.OnMouseEnter(sender, args);
            mouseOver = true;
        }

        protected override void OnMouseLeave(object sender, MouseEventArgs args)
        {
            base.OnMouseLeave(sender, args);
            mouseOver = false;
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            InitImages();
        }

        protected override void OnMouseButtonDown(object sender, MouseEventArgs args)
        {
            base.OnMouseButtonDown(sender, args);
            if(args.CurrentState.LeftButton == ButtonStates.Pressed)
            {
                mousePress = true;
            }
        }

        protected override void OnMouseButtonUp(object sender, MouseEventArgs args)
        {
            base.OnMouseButtonUp(sender, args);
            if(args.CurrentState.LeftButton == ButtonStates.Realesed)
            {
                mousePress = false;
            }
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn sich der <see cref="Text"/> ändert.
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
        /// Gibt die Textausrichtung zurück oder überschreibt dieses.
        /// </summary>
        public TextAlignment Alignment
        {
            get
            {
                return alignment;
            }
            set
            {
                alignment = value;
            }
        }

        /// <summary>
        /// Gibt den von diesem Steuerelement angezeigten Text zurück oder überschreibt diesen.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if(text != value)
                {
                    text = value;
                    OnTextChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der <see cref="Text"/> ändert.
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
