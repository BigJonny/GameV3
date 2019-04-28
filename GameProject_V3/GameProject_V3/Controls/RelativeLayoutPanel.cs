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
            Width = width;
            Height = height;
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
                    PointF p = new PointF(0.0f, 0.0f);
                    p.X = (float)control.Location.X / (float)oldWidth;
                    p.Y = (float)control.Location.Y / (float)oldHeight;
                    p.X = (float)Width * p.X;
                    p.Y = (float)Height * p.Y;
                    control.Location = new Point((int)p.X, (int)p.Y);
                    oldWidth = Width;
                    oldHeight = Height;
                }
            }
        }



    }
}
