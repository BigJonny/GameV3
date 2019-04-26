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

        private Bitmap drawArea;
        private Bitmap textDrawArea;
        private Panel itemControl;
        private bool showItems;

        private List<Label> items;
        private string text;
        private int selectedIndex;

        private static object textChangedKey = new object();
        private static object itemAddedKey = new object();
        private static object itemRemoveKey = new object();
        private static object selectedIndexChangedKey = new object();

        /// <summary>
        /// Erstellt eine neue leere ComboBox.
        /// </summary>
        public ComboBox()
        {
            items = new List<Label>();
            itemControl = new Panel();
            text = "";
            selectedIndex = -1;
            itemControl.BackColor = Color.White;
            itemControl.Visible = false;
            showItems = false;
            drawArea = new Bitmap(size.X, size.Y);
            Width = 200;
            Height = 50;
            textDrawArea = new Bitmap(Width, Height);
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            Graphics g = Graphics.FromImage(drawArea);
            g.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));

            Point[] downButton = new Point[3];
            downButton[0] = new Point(Width - 25, 0);
            downButton[1] = new Point(Width, 0);
            downButton[2] = new Point(Width - (25 / 2), Height);

            g.FillRectangle(new SolidBrush(Color.Gray), new RectangleF(Width - 25, 0, 25, Height));
            g.FillPolygon(new SolidBrush(Color.Black), downButton);
            g.DrawRectangle(Pens.Black, new Rectangle(Width - 25, 0, 24, Height - 1));

            Graphics textG = Graphics.FromImage(textDrawArea);
            textG.Clear(Color.Transparent);
            Font.DrawString(text, textG, new Rectangle(0, 0, Width - 25, Height), TextAlignment.Center);
            g.DrawImage(textDrawArea, new Rectangle(0, 0, Width - 25, Height));

            if(DrawBorder)
            {
                g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            graphics.DrawImage(drawArea, Bounds);
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
            if (showItems == true)
            {
                itemControl.Visible = true;
                itemControl.Eneabled = true;
            }
            else {
                itemControl.Visible = false;
                itemControl.Eneabled = false;
            }
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            drawArea = new Bitmap(Width, Height);
            textDrawArea = new Bitmap(Width - 25, Height);
            showItems = false;
            itemControl.Location = new Point(Location.X, Location.Y + Height);
            GenerateItemControl();
        }

        protected override void OnClick(object sender, MouseEventArgs args)
        {
            base.OnClick(sender, args);
            showItems = !showItems;
        }

        protected override void OnParrentChanged(object sender, EventArgs args)
        {
            base.OnParrentChanged(sender, args);
            if(Parrent != null)
            {
                Parrent.AddControl(itemControl);
                showItems = false;
                itemControl.Location = new Point(Location.X, Location.Y + Height);
                GenerateItemControl();
            }
        }

        protected override void OnLocationChanged(object sender, EventArgs args)
        {
            base.OnLocationChanged(sender, args);
            showItems = false;
            itemControl.Location = new Point(Location.X, Location.Y + Height);
            GenerateItemControl();
        }
        #endregion

        #region ItemManagment:
        /// <summary>
        /// Fügt dieser ComboBox ein neues Item hinzu.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(string item)
        {
            if(item != null)
            {
                Label l = new Label();
                l.Text = item;
                l.DrawBackgroundColor = true;
                l.GrowWidthText = false;
                l.Width = itemControl.Width;
                l.MouseEnter += new MouseEventHandler(EnterItem);
                l.MouseLeave += new MouseEventHandler(LeaveItem);
                l.Click += new MouseEventHandler(ItemClick);
                items.Add(l);
                GenerateItemControl();
                OnItemAdded(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Löscht ein Item an der gegebenen Stelle falls möglich.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItemAt(int index)
        {
            if (items.Count < index && index >= 0)
            {
                items.RemoveAt(index);
                GenerateItemControl();
                OnItemRemove(this, EventArgs.Empty);
            }
            else
                throw new IndexOutOfRangeException("Der angegebenene Index lag auserhalb " +
                    "des gültigen Bereichs");
        }

        /// <summary>
        /// Löscht das erste auftauchen eines Items mit dem gegebenen Text.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(string item)
        {
            int index = 0;
            bool hasfound = false;
            foreach(Label l in items)
            {
                if(l.Text == item)
                {
                    hasfound = true;
                    break;
                }
                index++;
            }
            if (hasfound == true)
            {
                RemoveItemAt(index);
            }
        }

        /// <summary>
        /// Generitert die Anzeige der Items neu.
        /// </summary>
        private void GenerateItemControl()
        {
            itemControl.ClearControls();
            Point pos = new Point(0, 0);
            foreach(Label l in items)
            {
                itemControl.AddControl(l);
                l.Location = pos;
                pos.Y += l.Height;
            }
            if(items.Count > 4)
            {
                itemControl.ScrollType = ContainerScroll.Vertical;
                itemControl.Height = Font.MeasureString("a").Y * 4;
                foreach(Label l in items)
                {
                    l.Width = itemControl.Width - itemControl.GetScrollBarFromType(ScrollType.Vertical).Width;
                }
                itemControl.GetScrollBarFromType(ScrollType.Vertical).Maximum = pos.Y - itemControl.Height;
                itemControl.GetScrollBarFromType(ScrollType.Vertical).ValueChanged += new EventHandler(OnScrollValueChanged);
            }else
            {
                itemControl.Height = pos.Y;
                itemControl.ScrollType = ContainerScroll.None;
            }
        }

        /// <summary>
        /// Tritt ein, wenn der Nutzer den Bildverlauf der Itemansicht verändert hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnScrollValueChanged(object sender, EventArgs args)
        {
            if(itemControl.GetScrollBarFromType(ScrollType.Vertical) != null)
            {
                if(itemControl.GetScrollBarFromType(ScrollType.Vertical).LastDirection == ScrollDirection.Down)
                {
                    foreach(Label l in items)
                    {
                        l.Location = new Point(l.Location.X, l.Location.Y + 1);
                    }
                }
                else
                {
                    foreach (Label l in items)
                    {
                        l.Location = new Point(l.Location.X, l.Location.Y - 1);
                    }
                }
            }
        }
        #endregion

        #region Item-Events:
        /// <summary>
        /// Tritt ein, wenn der Nutzer mit der Maus de nsichtbaren Bereich eines Items betritt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void EnterItem(object sender, EventArgs args)
        {
            (sender as Label).BackColor = Color.Gray;
        }

        /// <summary>
        /// Tritt ein, wenn der Nutzer mit Maus den sichtbaren Bereich eines Items verlässt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LeaveItem(object sender, EventArgs args)
        {
            (sender as Label).BackColor = BackColor;
        }

        /// <summary>
        /// Tritt ein, wenn der Nutzer mit de Maus auf ein Item klickt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ItemClick(object sender, EventArgs args)
        {
            Text = (sender as Label).Text;
            showItems = false;
            int index = 0;
            foreach(Label l in items)
            {
                if(l.Equals(sender as Label))
                {
                    selectedIndex = index;
                    OnSelectedIndexChanged(this, EventArgs.Empty);
                    break;
                }
            }
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn sich der Text des Steuerelements verändert hat.
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
        protected virtual void OnItemRemove(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[itemRemoveKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn der Nuzter ein neues Item ausgeählt hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnSelectedIndexChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[itemRemoveKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
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
                text = value;
                OnTextChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gibt den aktuell Index des aktuelle selektierten Elements zurück.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
        }

        /// <summary>
        /// Ein Ereignis, welches einrtitt, wenn ein neues Item hinzugefügt wird.
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
        public event EventHandler ItemRemoved
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

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der Index
        /// des aktuelle selektierten Items geändert hat.
        /// </summary>
        public event EventHandler SelectedIndexChanged
        {
            add
            {
                Events.AddHandler(selectedIndexChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(selectedIndexChangedKey, value);
            }
        }
        #endregion

    }
}
