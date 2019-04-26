using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_V3
{
    /// <summary>
    /// Die Eigenschaft eines Keys.
    /// </summary>
    public enum KeyStates
    {
        Pressed = 0, Realesed = 1
    }

    /// <summary>
    /// Ein Klasse, die den Zusatnd der Tastatur zu einem bestimmten Zeitpunkt speichern kann.
    /// </summary>
    public class KeyboardState
    {

        private List<Keys> pressedKeys;


        /// <summary>
        /// Erstellt einen neuen  KeyboardState.
        /// </summary>
        public KeyboardState()
        {
            pressedKeys = new List<Keys>();
        }


        public Keys[] GetPressedKeys ()
        {
            return pressedKeys.ToArray();
        }


        /// <summary>
        /// Fügt den übergebenen Key in die Liste ein.
        /// </summary>
        /// <param name="key"></param>
        public void AddPressedKey(Keys key)
        {
            if(pressedKeys.Contains(key) == false)
            {
                pressedKeys.Add(key);
            }
        }

        /// <summary>
        /// Löscht den übergebenen Key aus der Liste.
        /// </summary>
        /// <param name="key"></param>
        public void RemovePressedKey(Keys key)
        {
            pressedKeys.Remove(key);
        }

        /// <summary>
        /// Gibt eine Liste aller gedrückkten Tasten zurück und Konvertiert diese zu Strings.
        /// </summary>
        /// <returns></returns>
        public List<string> KeysToString ()
        {
            List<string> result = new List<string>();
            foreach(Keys k in pressedKeys)
            {
                result.Add(Enum.GetName(typeof(Keys), k));
            }
            return result;
        }

        /// <summary>
        /// /
        /// </summary>
        public bool IsAnyKeyDown
        {
            get
            {
                if(pressedKeys.Count > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}
