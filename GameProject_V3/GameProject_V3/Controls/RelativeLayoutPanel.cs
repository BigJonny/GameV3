using System;
using System.Collections.Generic;
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
            size = new System.Drawing.Point(width, height);
        }

        #region Overrides:
        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            ReOrderConrols();
        }

        protected override void OnLocationChanged(object sender, EventArgs args)
        {
            base.OnLocationChanged(sender, args);
            ReOrderConrols();
        }
        #endregion

        /// <summary>
        /// Ordnet die enthaltetnen Steuerlemente neu an.
        /// </summary>
        private void ReOrderConrols()
        {
            foreach(GameControl control in controls)
            {

            }
        }



    }
}
