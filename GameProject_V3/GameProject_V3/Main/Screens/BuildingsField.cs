using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main.Screens
{
    /// <summary>
    /// Ein Steuerlement, welches die Gebäude des Spieler anzeigen soll.
    /// </summary>
    public class BuildingsField : GameField
    {

        /// <summary>
        /// Erstellt ein neues Feld zum Anzeigen von Gebäuden.
        /// </summary>
        public BuildingsField()
        {
            DrawBorder = true;
        }


    }
}
