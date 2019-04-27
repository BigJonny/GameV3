using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Ein Klasse die die Abstände zum nächsten Steuerelement in einem Container speichert.
    /// </summary>
    public class Padding
    {

        private int left;
        private int right;
        private int top;
        private int bottum;

        #region Konstruktoren:
        /// <summary>
        /// Ersttelt ein neues Padding mit dem Wert 5.
        /// </summary>
        public Padding()
        {
            left = 5;
            right = 5;
            top = 5;
            left = 5;
        }

        /// <summary>
        /// Erstellt ein neues Padding mit dem Wert all.
        /// </summary>
        /// <param name="all"></param>
        public Padding(int all)
        {
            left = all;
            right = all;
            top = all;
            bottum = all;
        }

        /// <summary>
        /// Erstellt ein neues Padding mit den gegebenen Werten.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="buttom"></param>
        public Padding (int left, int right, int top, int buttom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottum = buttom;
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt den Padding zum linken Steuerlement an.
        /// </summary>
        public int Left
        {
            get
            {
                return left;
            }
        }

        /// <summary>
        /// Gibt den Paddingwert zum rechten Steuerelemtn an.
        /// </summary>
        public int Right
        {
            get
            {
                return right;
            }
        }

        /// <summary>
        /// Gibt den Paddingwert zum oberen Steuerelemt an.
        /// </summary>
        public int Top
        {
            get
            {
                return top;
            }
        }

        /// <summary>
        /// Gibt den Paddingwert zum unteren Steuerlelement an.
        /// </summary>
        public int Buttom
        {
            get
            {
                return bottum;
            }
        }
        #endregion
    }
}
