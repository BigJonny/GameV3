using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Eine Auflistung der möglichen ScrollBars eines Containers.
    /// </summary>
    public enum ContainerScroll { Vertical = 0, Horitontal = 1, None = 2, Both = 4}

    public abstract class Container : GameControl
    {

        protected List<GameControl> controls;
        private ContainerScroll scrollType;
        private ScrollBar[] scrollBars;

        private static object controlAddedKey = new object();
        private static object controlRemovedKey = new object();

        /// <summary>
        /// Erstellt einen neuen Container.
        /// </summary>
        public Container()
        {
            controls = new List<GameControl>();
            scrollBars = new ScrollBar[2];
            size = new Point(200, 200);
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            foreach(GameControl control in controls)
            {
                control.Draw(this.graphics, gameTime);
            }
            for(int i = 0; i < 2; i++)
            {
                if(scrollBars[i] != null)
                {
                    scrollBars[i].Draw(this.graphics, gameTime);
                }
            }
            graphics.DrawImage(DrawArea, Bounds);
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
            try
            {
                foreach (GameControl control in controls)
                {
                    control.Update(gameTime);
                }
                for (int i = 0; i < 2; i++)
                {
                    if (scrollBars[i] != null)
                    {
                        scrollBars[i].Update(gameTime);
                    }
                }
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region Control-Managment:
        /// <summary>
        /// Bestimmt den Status der ScrollBars neu.
        /// </summary>
        /// <param name="type"></param>
        private void GenerateContainerScroll(ContainerScroll type)
        {
            if(type == ContainerScroll.None)
            {
                scrollBars[0] = null;
                scrollBars[1] = null;
            }
            else if(type == ContainerScroll.Horitontal)
            {
                scrollBars[0] = null;

                scrollBars[1] = new ScrollBar(Controls.ScrollType.Horizontal);
                scrollBars[1].SetParrent(this);
            }
            else if(type == ContainerScroll.Vertical)
            {
                scrollBars[0] = new ScrollBar(Controls.ScrollType.Vertical);
                scrollBars[0].SetParrent(this);

                scrollBars[1] = null;
            }
            else
            {
                scrollBars[0] = new ScrollBar(Controls.ScrollType.Vertical);
                scrollBars[0].SetParrent(this);
                scrollBars[0].Height = scrollBars[0].Height - 20;

                scrollBars[1] = new ScrollBar(Controls.ScrollType.Horizontal);
                scrollBars[1].SetParrent(this);
            }
        }

        /// <summary>
        /// Fügt diesem Container ein neues Steuerlement hizu.
        /// </summary>
        /// <param name="control"></param>
        public void AddControl(GameControl control)
        {
            controls.Add(control);
            control.SetParrent(this);
            OnControlAdded(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fügt dem Container eine Reiehe von Steuerlementen hinzu.
        /// </summary>
        /// <param name="cs"></param>
        public void AddControls(List<GameControl> cs)
        {
            if (cs != null)
            {
                foreach (GameControl control in cs)
                {
                    AddControl(control);
                }
            }
        }

        /// <summary>
        /// Fügt diesem Container eine Reihe von Steuerelementen hinzu.
        /// </summary>
        /// <param name="cs"></param>
        public void AddControls(GameControl[] cs)
        {
            if(cs != null)
            {
                foreach (GameControl control in cs)
                {
                    AddControl(control);
                }
            }
        }

        /// <summary>
        /// Löscht alle in diesem Container entahltenen Steuerlemente.
        /// </summary>
        public void ClearControls()
        {
            foreach(GameControl control in controls)
            {
                control.SetParrent(null);
                OnControlRemoved(this, EventArgs.Empty);
            }
            controls.Clear();
        }

        /// <summary>
        /// Löscht das Steuerlement an der übergebenen Stelle, falls möglich.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveControlAt(int index)
        {
            if(index < controls.Count)
            {
                controls[index].SetParrent(null);
                controls.RemoveAt(index);
                OnControlRemoved(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Löscht das übergebene Steuerlement, wenn es in diesem Container enthalten ist.
        /// </summary>
        /// <param name="c"></param>
        public void RemoveControl(GameControl c)
        {
            int index = 0;
            bool hasfound = false;
            foreach(GameControl control in controls)
            {
                if(c.Equals(control))
                {
                    hasfound = true;
                    break;
                }
                index++;
            }
            if(hasfound)
            {
                RemoveControlAt(index);
            }
        }

        /// <summary>
        /// Zeigt das Steuerlelement über allen anderen in diesem Container.
        /// </summary>
        /// <param name="c"></param>
        public void BringToFront(GameControl c)
        {
            bool hasFound = false;
            foreach(GameControl control in controls)
            {
                if(c.Equals(control) == true)
                {
                    hasFound = true;
                    break;
                }
            }
            if(hasFound)
            {
                RemoveControl(c);
                AddControl(c);
            }
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn diesem Container ein neues Steuerelement hunzugefügt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnControlAdded(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[controlAddedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn ein Steuerelement aus diesem Container entfernt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnControlRemoved(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[controlRemovedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Hilfsfunktionen:
        /// <summary>
        /// Gibt die SrollBar des Containers zum entsprechenden <see cref="ScrollType"/> zurück.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ScrollBar GetScrollBarFromType (ScrollType type)
        {
            if(type == Controls.ScrollType.Vertical)
            {
                return scrollBars[0];
            }
            else
            {
                return scrollBars[1];
            }
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt die aktuelle Art des Bildschirmverlaufes dieses Containers zurück oder überschreibt diese.
        /// </summary>
        public ContainerScroll ScrollType
        {
            get
            {
                return scrollType;
            }
            set
            {
                scrollType = value;
                GenerateContainerScroll(scrollType);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn ein Steuerelement diesem Container hinzugefügt wird.
        /// </summary>
        public event EventHandler ControlAdded
        {
            add
            {
                Events.AddHandler(controlAddedKey, value);
            }
            remove
            {
                Events.RemoveHandler(controlRemovedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn ein Steuerelement von diesem Container entfernt wurde.
        /// </summary>
        public event EventHandler ConrtolRemoved
        {
            add
            {
                Events.AddHandler(controlRemovedKey, value);
            }
            remove
            {
                Events.RemoveHandler(controlRemovedKey, value);
            }
        }
        #endregion


    }
}
