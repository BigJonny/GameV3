using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3
{
    public static class GUIManager
    {
        #region Varaiblen:
        private static BitmapFont defaultFont;
        private static bool isInit = false;
        private static Screen screen;
        private static Game game;
        private static EventHandlerList Events;

        private static object screenChangedKey = new object();
        #endregion

        /// <summary>
        /// Initialisiert den GUIManager.
        /// </summary>
        /// <param name="_gameScreen"></param>
        public static void Initialize(Game _game)
        {
            Events = new EventHandlerList();
            BitmapFontManager.LoadFonts();
            defaultFont = BitmapFontManager.GetFontFromName("Calibri");
            isInit = true;
            game = _game;
            screen = new Screen();
        }

        #region Drawing/Updating:
        /// <summary>
        /// Zeichnet die GUI mit dem aktuellen Fenster.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public static void Draw(Graphics graphics, GameTime gameTime)
        {
            screen.Draw(graphics, gameTime);
        }

        /// <summary>
        /// Aktualisiert die GUI mit dem aktuellen Fenster.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            screen.Update(gameTime);
        }
        #endregion

        #region Hilfsfunktionen:
        /// <summary>
        /// Überschreibt das aktuelle <see cref="Screen"/>-Element der GUI.
        /// </summary>
        /// <param name="newScreen"></param>
        public static void SetCurrentScreen(Screen newScreen)
        {
            if(newScreen != null)
            {
                screen = newScreen;
                OnScreenChanged();
            }
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn ein neuer Screen gezeichnet werden muss.
        /// </summary>
        private static void OnScreenChanged()
        {
            EventHandler handler;
            handler = (EventHandler)Events[screenChangedKey];
            handler?.Invoke(null, EventArgs.Empty);
        }
        #endregion

        #region Eigenchaften:
        /// <summary>
        /// Gibt an, ob der GUIManager initialisiert wurde.
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return isInit;
            }
        }

        /// <summary>
        /// Gibt die Standartschriftart des GUIManagers zurück.
        /// </summary>
        public static BitmapFont DefaultFont
        {
            get
            {
                return defaultFont;
            }
        }

        /// <summary>
        /// Gibt das aktuelle <see cref="Screen"/>-Objekt zurück auf dem die GUI gezeichnet wird oder überschreibt dieses.
        /// </summary>
        public static Screen CurrentScreen
        {
            get
            {
                return screen;
            }
            set
            {
                if(value != null)
                {
                    screen = value;
                    OnScreenChanged();
                }
            }
        }

        /// <summary>
        /// Gibt das aktuelle Spiel zurück.
        /// </summary>
        public static Game CurrentGame
        {
            get
            {
                return game;
            }
        }

        /// <summary>
        /// Ein Ereignis, welches einritt, wenn ein neues <see cref="Screen"/>-Objekt gezeichnet werden soll.
        /// </summary>
        public static event EventHandler ScreenChanged
        {
            add
            {
                Events.AddHandler(screenChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(screenChangedKey, value);
            }
        }
        #endregion

    }
}
