using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Eine Erweiterung der Klasse <see cref="Control"/>, die die Ereignisse der Tastatur abfangen kann.
    /// </summary>
    public abstract class KeyEventControl : MouseEventControl
    {

        private KeyboardState state;
        private KeyboardState oldState;

        private static object keyDownKey = new object();
        private static object keyUpKey = new object();

        /// <summary>
        /// Erstellt ein neues KeyEventControl.
        /// </summary>
        public KeyEventControl()
        {
            oldState = KeyBoardInput.GetState();
        }


        protected override void UpdateKeyInput(GameTime gameTime)
        {
            state = KeyBoardInput.GetState();
            if(state.IsAnyKeyDown)
            {
                OnKeyDown(this, new KeyEventArgs(state, oldState));
                foreach(System.Windows.Forms.Keys key in oldState.GetPressedKeys())
                {
                    if(state.GetPressedKeys().Contains(key) == false)
                    {
                        OnKeyUp(this, new KeyEventArgs(state, oldState));
                    }
                }
            }
            oldState = state;
        }

        #region Events:
        /// <summary>
        /// Tritt ein, wenn eine Taste gedrückt wurde.
        /// </summary>
        protected virtual void OnKeyDown(object sender, KeyEventArgs args)
        {
            KeyEventHandler handler;
            handler = (KeyEventHandler)Events[keyDownKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn eine Taste losgelassen wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnKeyUp(object sender, KeyEventArgs args)
        {
            KeyEventHandler handler;
            handler = (KeyEventHandler)Events[keyUpKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Ein Ereignis, welches ausgelöst wird, wenn eine Taste gedrückt wird wenn das Steuerelement den Fokus hat.
        /// </summary>
        public event KeyEventHandler KeyDown
        {
            add
            {
                Events.AddHandler(keyDownKey, value);
            }
            remove
            {
                Events.RemoveHandler(keyDownKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn eine Taste losgelassen wird, während das Steuerlement den Fokus hat.
        /// </summary>
        public event KeyEventHandler KeyUp
        {
            add
            {
                Events.AddHandler(keyUpKey, value);
            }
            remove
            {
                Events.RemoveHandler(keyUpKey, value);
            }
        }
        #endregion
    }
}
