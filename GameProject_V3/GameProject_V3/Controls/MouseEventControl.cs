using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Eine Klasse, die die <see cref="Control"/>-Klasse um alle Eigenchaften erweitertet, die mit der Maus zu tun haben.
    /// </summary>
    public abstract class MouseEventControl : Control
    {


        private MouseState state;
        private MouseState oldState;

        private static object mouseLeaveKey = new object();
        private static object mouseEnterKey = new object();
        private static object mouseMoveKey = new object();
        private static object mouseHoverKey = new object();
        private static object clickKey = new object();
        private static object mouseButtonDownKey = new object();
        private static object mouseButtonUpKey = new object();
        private static object mouseDoubleClickKey = new object();
        private static object scrollKey = new object();

        /// <summary>
        /// Erstellt ein neues MouseEventControl.
        /// </summary>
        public MouseEventControl()
        {
            oldState = MouseInput.GetState();
        }

        protected override void UpdateMouse(GameTime gameTime)
        {
            state = MouseInput.GetState();
            if(IsPointInside(state.Position))
            {
                if(IsPointInside(oldState.Position) == false)
                {
                    OnMouseEnter(this, new MouseEventArgs(state, oldState));
                }
                if(state.Position == oldState.Position)
                {
                    OnMouseHover(this, new MouseEventArgs(state, oldState));
                }
                if(state.Scroll != oldState.Scroll)
                {
                    OnScroll(this, new MouseEventArgs(state, oldState));
                }
                else
                {
                    OnMouseMove(this, new MouseEventArgs(state, oldState));
                }

                if(state.AnyKeyPressed == true)
                {
                    OnMouseButtonDown(this, new MouseEventArgs(state, oldState));
                    if (state.LeftButton == ButtonStates.Pressed)
                    {
                        if (oldState.LeftButton == ButtonStates.Realesed)
                        {
                            OnClick(this, new MouseEventArgs(state, oldState));
                        }
                    }
                }
                if(oldState.LeftButton == ButtonStates.Pressed && state.LeftButton == ButtonStates.Realesed)
                {
                    OnMouseButtonUp(this, new MouseEventArgs(state, oldState));
                }
                if (oldState.MiddleButton == ButtonStates.Pressed && state.MiddleButton == ButtonStates.Realesed)
                {
                    OnMouseButtonUp(this, new MouseEventArgs(state, oldState));
                }
                if(oldState.RightButton == ButtonStates.Pressed && state.RightButton == ButtonStates.Realesed)
                {
                    OnMouseButtonUp(this, new MouseEventArgs(state, oldState));
                }
            }
            else
            {
                if(IsPointInside(oldState.Position))
                {
                    OnMouseLeave(this, new MouseEventArgs(state, oldState));
                }
            }
            oldState = state;
        }


        #region Events:
        /// <summary>
        /// Tritt ein, wenn die Maus den sichtbaren Bereichg des Steuerelements betritt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseEnter(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseEnterKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn die Maus den sichtbaren Bereich des Steuerlements verlässt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseLeave(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseLeaveKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn die Maus über dem sichtbaren Bereich des Steuerelements bewigt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseMove(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseMoveKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich die Maus über dem sichtbaren Bereich des Steuerelements befindet und sich 
        /// mindestens für einen Frame nicht bewegt hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseHover(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseHoverKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn auf das Steuerlement mit der linken Maustaste gedrück wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnClick(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[clickKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn mit der linken Maustaste zweimal innerhalt von zwei Sekunden auf das Steuerelemente geklcikt wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnDoubleClick(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseDoubleClickKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn eine Maustaste gedrückt ist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseButtonDown(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseButtonDownKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn eine Maustaste losgelassen wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseButtonUp(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[mouseButtonUpKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn der Nutzer mit der Maus über dem sichtbaren Bereich des Steuerelements
        /// ist und das Scrollrad der Maus bewegt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnScroll(object sender, MouseEventArgs args)
        {
            MouseEventHandler handler;
            handler = (MouseEventHandler)Events[scrollKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Ein Ereignis, welches ausgelöst wird, wenn die Maus den sichtbaren Bereich des Steuerlements betritt.
        /// </summary>
        public event MouseEventHandler MouseEnter
        {
            add
            {
                Events.AddHandler(mouseEnterKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseEnterKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn die Maus den sichbaren Bereich des Steuerelements verlässt.
        /// </summary>
        public event MouseEventHandler MouseLeave
        {
            add
            {
                Events.AddHandler(mouseLeaveKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseLeaveKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn die Maus über dem sichtbaren Bereich des Steuerelements bewegt wird.
        /// </summary>
        public event MouseEventHandler MouseMove
        {
            add
            {
                Events.AddHandler(mouseMoveKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseMoveKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich die Maus über dem sichtbaren Bereich des Steuerelements
        /// befindet und sich für mindestens einen Frame nicht bewegt.
        /// </summary>
        public event MouseEventHandler MouseHover
        {
            add
            {
                Events.AddHandler(mouseHoverKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseHoverKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches ausgelöst wirtd, wenn mit der linken Maustaste auf den sichtbaren Bereich des
        /// Steuerelements geklickt wurde.
        /// </summary>
        public event MouseEventHandler Click
        {
            add
            {
                Events.AddHandler(clickKey, value);
            }
            remove
            {
                Events.RemoveHandler(clickKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches ausgelöst wrid, wenn mit de linken Maustaste innerhalb von zwei Sekunden 
        /// auf den sichtbaren Bereich des Steuerelements geklickt wurde.
        /// </summary>
        public event MouseEventHandler DoubleClick
        {
            add
            {
                Events.AddHandler(mouseDoubleClickKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseDoubleClickKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn eine Maustataste über dem sichtbaren Bereich des Steuerlements geklickt wurde.
        /// </summary>
        public event MouseEventHandler MouseButtonDown
        {
            add
            {
                Events.AddHandler(mouseButtonDownKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseButtonDownKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn eine Maustaste losgelassen wurde und sich die Maus über dem sichtbaren Bereich des Steuerlements befindet.
        /// </summary>
        public event MouseEventHandler MouseButtonUp
        {
            add
            {
                Events.AddHandler(mouseButtonUpKey, value);
            }
            remove
            {
                Events.RemoveHandler(mouseButtonUpKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignise, welches eintritt, wenn der Nutzer mit der Maus über dem sichtbaren
        /// Bereich des Steuerelemetns ist und das Mausrad bewegt.
        /// </summary>
        public event MouseEventHandler Scoll
        {
            add
            {
                Events.AddHandler(scrollKey, value);
            }
            remove
            {
                Events.RemoveHandler(scrollKey, value);
            }
        }
        #endregion
    }
}
