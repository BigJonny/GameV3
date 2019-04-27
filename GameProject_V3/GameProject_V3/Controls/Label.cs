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
        private bool growWidthText;

        private static object textChangedKey = new object();
        #endregion

        /// <summary>
        /// Erstellt ein neues leeres Label.
        /// </summary>
        public Label() : base()
        {
            text = "";
            Width = 100;
            Height = 50;
            growWidthText = true;
            BackColor = Color.Transparent;
            DrawBorder = false;
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            Font.DrawString(Text, this.graphics, new Point(0, 0));
            graphics.DrawImage(DrawArea, Bounds);
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
                TextSizeChanged();
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
