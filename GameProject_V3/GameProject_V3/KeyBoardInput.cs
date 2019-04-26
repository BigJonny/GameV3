using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_V3
{
    /// <summary>
    /// Ein statische Klasse, die Tastatur Eingageben registriert und speichert.
    /// </summary>
    public static class KeyBoardInput
    {

        private static KeyboardState state;

        /// <summary>
        /// Initialisiert den KeyboardInput.
        /// </summary>
        public static void Initialize()
        {
            state = new KeyboardState();
        }

        /// <summary>
        /// Tritt ein, wenn eine Taste gedrück wird undder aktuelle Status der Tastatur aktualisiert werden muss.
        /// </summary>
        /// <param name="key"></param>
        public static void AddPressedKey(Keys key)
        {
            state.AddPressedKey(key);
        }


        /// <summary>
        /// Tritt ein, wenn eine Taste losgelassen wurde und der aktuelle Status der Tastatur aktualisiert werden muss.
        /// </summary>
        /// <param name="key"></param>
        public static void RemovePressedKey(Keys key)
        {
            state.RemovePressedKey(key);
        }

        /// <summary>
        /// Gibt den Tastaturstatus zurück.
        /// </summary>
        /// <returns></returns>
        public static KeyboardState GetState()
        {
            KeyboardState result = new KeyboardState();
            if(state.GetPressedKeys() != null)
            {
                foreach (Keys k in state.GetPressedKeys())
                {
                    result.AddPressedKey(k);
                }
            }
            return result;
        }

    }
}
