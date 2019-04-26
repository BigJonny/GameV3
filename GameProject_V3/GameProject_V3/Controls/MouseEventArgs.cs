using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{

    /// <summary>
    /// Ein EventHandler, der Informationen der Maus mit übertragt.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void MouseEventHandler(object sender, MouseEventArgs args);

    /// <summary>
    /// Eine Klasse, die die Informationen über die Maus speicherz um sie dem MouseEvenHandler zu übergeben.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {


        private MouseState state;
        private MouseState oldState;

        /// <summary>
        /// Erstellt ein neues MouseEventArgs-Object.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="oldState"></param>
        public MouseEventArgs(MouseState state, MouseState oldState)
        {
            this.state = state;
            this.oldState = oldState;
        }


        /// <summary>
        /// Gibt den zum Zeitpunkt des Events aktuellen Status der Mouse zurück.
        /// </summary>
        public MouseState CurrentState
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// Gibt den im letzten Frame existierenden Status der Mouse zurück.
        /// </summary>
        public MouseState PreviousState
        {
            get
            {
                return oldState;
            }
        }

    }
}
