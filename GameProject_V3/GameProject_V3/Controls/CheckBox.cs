using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    public class CheckBox : GameControl
    {
        private bool checkState;
        private string text;

        private Bitmap checkedImage;
        private Bitmap notCheckedImage;

        private static object textChangedKey = new object();
        private static object checkedChangedKey = new object();

        /// <summary>
        /// Erstellt eine leere CheckBox.
        /// </summary>
        public CheckBox()
        {
            text = "";
            checkState = false;
            DrawBorder = false;
            Width = 200;
            Height = 50;
            checkedImage = ContentLoader.LoadImage("CheckedImage.png");
            notCheckedImage = ContentLoader.LoadImage("NotCheckedImage.png");
        }

        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);

            if(checkState == true)
            {
                this.graphics.DrawImage(checkedImage, new Rectangle(0, 0, Height, Height));
            }
            else
            {
                this.graphics.DrawImage(notCheckedImage, new Rectangle(0, 0, Height, Height));
            }

            if(DrawBorder == true)
            {
                this.graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            Font.DrawString(text, this.graphics, new Rectangle(Height + 10, 0,
                Width - (Height + 10), Height), TextAlignment.Left);
            graphics.DrawImage(DrawArea, Bounds);
        }


        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
        }

        protected override void OnClick(object sender, MouseEventArgs args)
        {
            base.OnClick(sender, args);
            checkState = !checkState;
        }

        #region Events:
        /// <summary>
        /// Tritt ein, wenn sich der Text der CheckBox geändert hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnTextChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[textChangedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich der Checked-Status dieses Steuerelements verändert wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnCheckedChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[checkedChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt den angezeigten Text zurück oder überschreibt diesen.
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
                OnTextChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gibt an, ob das Steuerelement den Status checked oder not checked aufweist oder
        /// überschreibt diese Eigenschaft.
        /// </summary>
        public bool Checked
        {
            get
            {
                return checkState;
            }
            set
            {
                if(value != checkState)
                {
                    checkState = value;
                    OnCheckedChanged(this, EventArgs.Empty);
                }
            }
        }
        #endregion

    }
}
