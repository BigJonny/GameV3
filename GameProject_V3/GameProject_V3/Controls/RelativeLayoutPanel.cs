using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Ein Container, der Steuerelemente raltic zu deseen Größe positioniert.
    /// </summary>
    public class RelativeLayoutPanel : Container
    {

        //TODO: RelativeLayoutPanel muss vervollständigt werden.
        private int oldWidth;
        private int oldHeight;

        /// <summary>
        /// Ertellt ein neues leeres RelativLayoutPanel.
        /// </summary>
        public RelativeLayoutPanel(int width, int height)
        {
            oldWidth = width;
            oldHeight = height;
            size = new Point(width, height);
        }

        #region Overrides:
        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            ReOrderConrols();
        }
        #endregion

        /// <summary>
        /// Ordnet die enthaltetnen Steuerlemente neu an.
        /// </summary>
        private void ReOrderConrols()
        {
            if (oldHeight != Width || oldHeight != Height)
            {
                foreach (GameControl control in controls)
                {
                    Point p = new Point(0, 0);
                    p.X = control.Location.X / oldHeight;
                    p.Y = control.Location.Y / oldHeight;
                    p.X = p.X * Width;
                    p.Y = p.Y * Height;
                    control.Location = p;
                    oldWidth = Width;
                    oldHeight = Height;
                }
            }
        }



    }
}
