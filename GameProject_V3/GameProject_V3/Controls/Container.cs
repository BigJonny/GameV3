﻿using System;
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

        private List<GameControl> controls;
        private Bitmap drawArea;
        private ContainerScroll scrollType;
        private ScrollBar[] scrollBars;

        /// <summary>
        /// Erstellt einen neuen Container.
        /// </summary>
        public Container()
        {
            controls = new List<GameControl>();
            scrollBars = new ScrollBar[2];
            Width = 200;
            Height = 200;
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            Graphics g = Graphics.FromImage(drawArea);
            if(BackgroundImage != null)
            {
                g.DrawImage(BackgroundImage, new Rectangle(0, 0, Width, Height));
            }
            foreach(GameControl control in controls)
            {
                control.Draw(g, gameTime);
            }
            for(int i = 0; i < 2; i++)
            {
                if(scrollBars[i] != null)
                {
                    scrollBars[i].Draw(g, gameTime);
                }
            }
            graphics.DrawImage(drawArea, Bounds);
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
            foreach(GameControl control in controls)
            {
                control.Update(gameTime);
            }
            for(int i = 0; i < 2; i++)
            {
                if(scrollBars[i] != null)
                {
                    scrollBars[i].Update(gameTime);
                }
            }
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            drawArea = new Bitmap(Width, Height);
            Graphics graphics = Graphics.FromImage(drawArea);
            graphics.Clear(BackColor);
            graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(0, 0, Width - 1, Height - 1));
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
        #endregion


    }
}