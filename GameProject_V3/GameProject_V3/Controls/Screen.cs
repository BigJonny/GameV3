using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    public class Screen : Container
    {

        //TODO: Größe des Screens darf nich verändert werden!
        
        /// <summary>
        /// Erstellt einen leeren standart Screen.
        /// </summary>
        public Screen()
        {
            BackColor = Color.CornflowerBlue;
            Location = new Point(0, 0);
            Width = GUIManager.CurrentGame.Resulution.X;
            Height = GUIManager.CurrentGame.Resulution.Y;
        }

        /// <summary>
        /// Erstellt einen neuen leeren Screen mit der übergebenen Hintergrundfarbe.
        /// </summary>
        /// <param name="backColor"></param>
        public Screen(Color backColor)
        {
            BackColor = backColor;
            Location = new Point(0, 0);
            Width = GUIManager.CurrentGame.Resulution.X;
            Height = GUIManager.CurrentGame.Resulution.Y;
        }

    }
}
