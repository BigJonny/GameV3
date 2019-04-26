using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{

    /// <summary>
    /// Ein EventHandler, der das <see cref="KeyEventArgs"/>-Argument bekommen kann.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void KeyEventHandler(object sender, KeyEventArgs args);

    /// <summary>
    /// Ein Argument, welches dem KeyEventHandler übergebenen werden kann, um Ereignisse der Tastatur abzuarbeiten.
    /// </summary>
    public class KeyEventArgs : EventArgs 
    {


        private KeyboardState state;
        private KeyboardState oldState;

        /// <summary>
        /// Erstellt ein neues KeyEvent-Argument.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="oldState"></param>
        public KeyEventArgs (KeyboardState state, KeyboardState oldState)
        {
            this.state = state;
            this.oldState = oldState;
        }

        /// <summary>
        /// Gibt den Status der Tastatur zum aktuellen Frame zurück.
        /// </summary>
        public KeyboardState CurrentState
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// Gibt den Status der Tasatur im leztzen Frame zurück.
        /// </summary>
        public KeyboardState PreviousState
        {
            get
            {
                return oldState;
            }
        }

    }
}
