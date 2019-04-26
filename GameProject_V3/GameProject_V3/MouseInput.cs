using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{

    public enum MouseButtonIDs
    {
        Left = 0, Middle = 1, Right = 2
    }

    public static class MouseInput
    {


        private static MouseState state;
        private static Point screenpos;
        private static Point parrentSize;

        /// <summary>
        /// Initialisiert den MouseInput.
        /// </summary>
        public static void Initialize()
        {
            state = new MouseState();
            screenpos = new Point(0, 0);
            parrentSize = new Point(0, 0);
        }

        /// <summary>
        /// Gibt den aktuellen Status der Maus zurück.
        /// </summary>
        /// <returns></returns>
        public static MouseState GetState()
        {
            MouseState result = new MouseState();
            result.LeftButton = state.LeftButton;
            result.MiddleButton = state.MiddleButton;
            result.RightButton = state.RightButton;
            result.Position = state.Position;
            return result;
        }

        #region Events:
        /// <summary>
        /// Bestimmt die Position der Maus auf dem Bildschirm.
        /// </summary>
        /// <param name="pos"></param>
        public static void SetPosition(Point pos, Bitmap drawArea)
        {
            state.Position = new Point(pos.X, pos.Y);
            state.Position = ImageHelper.TranslateMousePos(state.Position, drawArea, parrentSize);
        }

        /// <summary>
        /// Bestimmt den Status eines Mausbuttons.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="button"></param>
        public static void SetButton(ButtonStates buttonState, MouseButtonIDs button)
        {
            if(button == MouseButtonIDs.Left)
            {
                state.LeftButton = buttonState;
            }
            else if(button == MouseButtonIDs.Middle)
            {
                state.MiddleButton = buttonState;
            }
            else if(button == MouseButtonIDs.Right)
            {
                state.RightButton = buttonState;
            }
        }
        
        /// <summary>
        /// Bestimmt den Scroll-Wert der Maus.
        /// </summary>
        /// <param name="value"></param>
        public static void SetScrollValue(int value)
        {
            state.Scroll = value;
        }

        /// <summary>
        /// Bestimmt die Position des Spielbildschirms neu.
        /// </summary>
        /// <param name="_screenPos"></param>
        public static void SetScreenPos(Point _screenPos)
        {
            screenpos = _screenPos;
        }

        /// <summary>
        /// Bestimmt die Größe des Spielbildschirms neu.
        /// </summary>
        /// <param name="size"></param>
        public static void SetParrentSize(Point size)
        {
            parrentSize = size;
        }
        #endregion

    }
}
