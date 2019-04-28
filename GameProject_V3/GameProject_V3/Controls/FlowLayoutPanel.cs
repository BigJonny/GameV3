using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Gibt die Richtungen an in derer die Steuerelemente des FlowLayouts
    /// angezeigt werden.
    /// </summary>
    public enum FlowDireciont
    {
        UpDown = 0, RightLeft = 1
    }

    /// <summary>
    /// Ein Container der Steuerlemente in einer betsimmten Richtung aneinander reit
    /// </summary>
    public class FlowLayoutPanel : Container
    {

        private FlowDireciont direciont;
        private bool fullSize;

        /// <summary>
        /// Erstellt ein neues FlowLayoutPanel
        /// </summary>
        public FlowLayoutPanel()
        {
            direciont = FlowDireciont.RightLeft;
            fullSize = false;
        }

        #region Overrides:
        protected override void OnControlAdded(object sender, EventArgs args)
        {
            base.OnControlAdded(sender, args);
            ReOrderControls();
        }

        protected override void OnControlRemoved(object sender, EventArgs args)
        {
            base.OnControlRemoved(sender, args);
            ReOrderControls();
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            ReOrderControls();
        }
        #endregion

        #region Hilfsfunktionen:
        /// <summary>
        /// Ordnet die Steuerelemente des Containers neu.
        /// </summary>
        private void ReOrderControls()
        {
            if(direciont == FlowDireciont.RightLeft)
            {
                Point pos = new Point(0, 0);
                foreach(GameControl control in controls)
                {
                    pos.X += control.Padding.Left;
                    control.Location = pos;
                    pos.X += control.Width + control.Padding.Right;
                    if(fullSize == true)
                    {
                        control.Height = this.Height;
                    }
                }
            }
            else
            {
                Point pos = new Point(0, 0);
                foreach(GameControl control in controls)
                {
                    pos.Y += control.Padding.Top;
                    control.Location = pos;
                    pos.Y += control.Padding.Buttom;
                    if(fullSize == true)
                    {
                        control.Width = Width;
                    }
                }
            }
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt die Ausrichtung der Steuerlemente zurück oder überschreibt diese.
        /// </summary>
        public FlowDireciont Direction
        {
            get
            {
                return direciont;
            }
            set
            {
                direciont = value;
                ReOrderControls();
            }
        }

        /// <summary>
        /// Gibt an, ob enthaltene Steuerlemente die Breite bzw Höhe des Containers voll
        /// ausfüllen sollen.
        /// </summary>
        public bool FulSize
        {
            get
            {
                return fullSize;
            }
            set
            {
                fullSize = value;
                ReOrderControls();
            }
        }
        #endregion

    }
}
