using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{

    public enum ButtonStates
    {
        Pressed = 0, Realesed = 1,
    }

    /// <summary>
    /// Eine Klasse, die die alle Eigenschaften besitzt, um den Status der Maus zu einem
    /// bestimmten Zeitpunkt festzuhalten.
    /// </summary>
    public class MouseState
    {

        private Point position = new Point(0, 0);
        private ButtonStates leftButton = ButtonStates.Realesed;
        private ButtonStates rightButton = ButtonStates.Realesed;
        private ButtonStates middleButton = ButtonStates.Realesed;
        private int scroll = 0;

        /// <summary>
        /// Erstellt einen neuen Mausstatus.
        /// </summary>
        public MouseState()
        {
            position = new Point(0, 0);
            leftButton = ButtonStates.Realesed;
            rightButton = ButtonStates.Realesed;
            middleButton = ButtonStates.Realesed;
            scroll = 0;
        }

        #region Eigenschaften:
        /// <summary>
        /// Gibt die Position der Maus zurück.
        /// </summary>
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// Gibt den Status der linken Maustatse zurück.
        /// </summary>
        public ButtonStates LeftButton
        {
            get
            {
                return leftButton;
            }
            set
            {
                leftButton = value;
            }
        }

        /// <summary>
        /// Gibt den Status der rechten Maustaste zurück.
        /// </summary>
        public ButtonStates RightButton
        {
            get
            {
                return rightButton;
            }
            set
            {
                rightButton = value;
            }
        }

        /// <summary>
        /// Gibt den Status der mittleren Maustaste zurück.
        /// </summary>
        public ButtonStates MiddleButton
        {
            get
            {
                return middleButton;
            }
            set
            {
                middleButton = value;
            }
        }

        /// <summary>
        /// Gibt den Scroll-Wert der Maus zurück.
        /// </summary>
        public int Scroll
        {
            get
            {
                return scroll;
            }
            set
            {
                scroll = value;
            }
        }

        /// <summary>
        /// Gibt true zurück, wenn irgendeine Maustaste gedrückt ist.
        /// </summary>
        public bool AnyKeyPressed
        {
            get
            {
                if(LeftButton == ButtonStates.Pressed ||RightButton == ButtonStates.Pressed ||MiddleButton == ButtonStates.Pressed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

    }
}
