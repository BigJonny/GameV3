using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    public class Panel : Container
    {

        /// <summary>
        /// Erstellt ein neues Panel.
        /// </summary>
        public Panel()
        {
            BackColor = Color.Gray;
        }

        /// <summary>
        /// Erstellt ein Panel mit den gegebenen Dimensionen.
        /// </summary>
        /// <param name="bounds"></param>
        public Panel(Rectangle bounds)
        {
            BackColor = Color.CornflowerBlue;
            Location = bounds.Location;
            Size = bounds.Size.ToPoint();
        }

    }
}
