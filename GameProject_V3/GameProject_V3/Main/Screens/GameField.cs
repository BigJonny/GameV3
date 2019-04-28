using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main.Screens
{
    /// <summary>
    /// Stellt ein Feld auf dem Spielbrett dar.
    /// </summary>
    public abstract class GameField : PictureBox
    {

        /// <summary>
        /// Erstellt ein neues GameField.
        /// </summary>
        public GameField()
        {
            BackColor = Color.Transparent;
        }


    }
}
