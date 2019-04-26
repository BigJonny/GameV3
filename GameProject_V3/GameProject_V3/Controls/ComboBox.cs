using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{

    /// <summary>
    /// Implementiert eine simple ComboBox.
    /// </summary>
    public class ComboBox : GameControl
    {

        private List<string> items;
        private string text;
        private Bitmap drawArea;
        private Bitmap textDrawArea;
        private int downButtonWidth;
        private int selectedIndex;
        private TextAlignment alignment;
        private Panel itemControl;
        private bool showItems;

        private static object textChangedKey = new object();
        private static object itemAddedKey = new object();
        private static object itemRemoveKey = new object();
        

        /// <summary>
        /// Erstellt eine neue leere ComboBox.
        /// </summary>
        public ComboBox()
        {
            items = new List<string>();
            text = "";
            Width = 200;
            Height = 50;
            downButtonWidth = 20;
            selectedIndex = -1;
            alignment = TextAlignment.Center;
            itemControl = new Panel();
            showItems = false;
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            Graphics g = Graphics.FromImage(drawArea);

            g.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));

            Point[] down = new Point[3];
            down[0] = new Point(Width - downButtonWidth + (downButtonWidth / 2), Height);
            down[1] = new Point(Width - downButtonWidth, 0);
            down[2] = new Point(Width, 0);

            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(Width - downButtonWidth, 0, downButtonWidth, Height));
            g.FillPolygon(new SolidBrush(Color.Black), down);
            g.DrawRectangle(Pens.Black, new Rectangle(Width - downButtonWidth, 0, downButtonWidth, Height));

            Font.DrawString(text, Graphics.FromImage(textDrawArea), 
                new Rectangle(0, 0, textDrawArea.Width, textDrawArea.Height), alignment);
            g.DrawImage(textDrawArea, new Rectangle(0, 0, textDrawArea.Width, textDrawArea.Height));

            graphics.DrawImage(drawArea, Bounds);
            g.Dispose();
        }


        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
            MouseState state = MouseInput.GetState();
            if(IsPointInside(state.Position) == false)
            {
                if(state.LeftButton == ButtonStates.Pressed || state.RightButton == ButtonStates.Pressed)
                {
                    showItems = false;
                }
            }
            if(showItems == true)
            {
                itemControl.Update(gameTime);
            }
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            drawArea = new Bitmap(Width, Height);
            textDrawArea = new Bitmap(Width - downButtonWidth, Height);
        }

        protected override void OnParrentChanged(object sender, EventArgs args)
        {
            base.OnParrentChanged(sender, args);
            if (GUIManager.CurrentScreen != null)
            {
                itemControl = new Panel();
                itemControl.Location = new Point(GetScreenLocation().X, GetScreenLocation().Y + Height);
                itemControl.Width = Width;
                itemControl.Height = 300;
                GUIManager.CurrentScreen.AfterPaint += new DrawEventHandler(DrawPanel);
            }
        }

        protected override void OnClick(object sender, MouseEventArgs args)
        {
            base.OnClick(sender, args);
            Rectangle downButtonScreenBounds = new Rectangle(GetScreenLocation(), new Size(downButtonWidth, Height));
            downButtonScreenBounds.X += Width - downButtonWidth;
            if (IsPointInside(args.CurrentState.Position, downButtonScreenBounds));
            {
                showItems = true;
            }
        }
        #endregion

        #region Hilfsfunktionen:
        /// <summary>
        /// Zeichnet das Panel auf dem die Steuerlemente angezeigt werden sollen.
        /// </summary>
        private void DrawPanel(object sender, DrawEventArgs args)  
        {
            Console.WriteLine("Draw ITemControl");
            if (showItems == true)
            {
                itemControl.Draw(args.GraphicsDevice, args.Time);
                Console.WriteLine("Draw ITemControl");
            }
        }

        #endregion

        #region Item-Managment:
        /// <summary>
        /// Fügt der ComboBox ein neues Item hinzu.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(string item)
        {
            if(item != null)
            {
                items.Add(item);
                OnItemAdded(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Löscht das Item an der Stelle index, falls möglich.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItemAdd(int index)
        {
            if (items.Count > index && index >= 0)
            {
                items.RemoveAt(index);
                OnItemRemoved(this, EventArgs.Empty);
            }
            else
                throw new IndexOutOfRangeException("Der Index lag ausserhalb des gültigen" +
                    "Breichs: (Index: " + index + ", Items.Count: " + items.Count + ")");
        }

        /// <summary>
        /// Löscht das Item an der gegebenen Stelle.
        /// </summary>
        /// <param name="item"></param>
        public void RomveItem(string item)
        {
            int index = 0;
            bool hasfound = false;
            foreach(string s in items)
            {
                if(s == item)
                {
                    hasfound = true;
                    break;
                }
                index++;
            }
            if(hasfound == true)
            {
                RemoveItemAdd(index);
            }
        }

        /// <summary>
        /// Fügt dem Steuerelement eine Reihe von Items hinzu.
        /// </summary>
        /// <param name="range"></param>
        public void AddItems(List<string> range)
        {
            if(range != null)
            {
                foreach (string item in range)
                {
                    AddItem(item);
                }
            }
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn ein neues Item hinzugefügt wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnItemAdded(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[itemAddedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn ein Item gelöscht wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnItemRemoved(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[itemRemoveKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der Text des Steuerlements geändert hat.
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
        /// Gibt den von diesesm Steuerlement repäsentierten Text zurück oder überschreibt diesen.
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
        /// Gibt die Ausrichtung der <see cref="Text"/>-Eigenschaft zurück oder
        /// überschreibt diese.
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
        /// Ein Ereignis, welches eintritt, wenn ein neues Item hinzugefügt wurde.
        /// </summary>
        public event EventHandler ItemAdded
        {
            add
            {
                Events.AddHandler(itemAddedKey, value);
            }
            remove
            {
                Events.RemoveHandler(itemAddedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn ein Item gelöscht wurde.
        /// </summary>
        public event EventHandler ItemRemove
        {
            add
            {
                Events.AddHandler(itemRemoveKey, value);
            }
            remove
            {
                Events.RemoveHandler(itemRemoveKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich die <see cref="Text"/>-Eigenschaft geändert hat.
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
