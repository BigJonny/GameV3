using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main
{
    /// <summary>
    /// Speichert alle möglichen Spiel-Statuse.
    /// </summary>
    public enum GameState
    {
        MainMenu = 0, OptionsMenu = 1,
    }

    public static class GameStateManager
    {

        private static GameState state;
        private static Screens.MainScreen mainScreen;
        private static Screens.OptionsScreen optionScreen;
        private static Screen currentScreen;
        
        /// <summary>
        /// Initialisiert den GameStateManager.
        /// </summary>
        public static void Initialize()
        {
            state = GameState.MainMenu;
            mainScreen = new Screens.MainScreen();
            optionScreen = new Screens.OptionsScreen();
            ChangeState(GameState.MainMenu);
        }

        /// <summary>
        /// Zwingt das Spiel den neue Spielstatus anzunehemen.
        /// </summary>
        /// <param name="state"></param>
        public static void ChangeState(GameState newState)
        {
            state = newState;
            if(newState == GameState.MainMenu)
            {
                currentScreen = mainScreen;
            }
            else if(newState == GameState.OptionsMenu)
            {
                currentScreen = optionScreen;
            }
            GUIManager.SetCurrentScreen(currentScreen);
        }

        /// <summary>
        /// Zeichnet den aktuellen Status neu.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        public static void Draw(Graphics graphics, GameTime gameTime)
        {
            GUIManager.Draw(graphics, gameTime);
        }

        /// <summary>
        /// Aktualisert den aktuellen Spielstatus.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            GUIManager.Update(gameTime);
        }

        /// <summary>
        /// Wechselt die Auflüsung des Spiels auf den übegebenen Wert.
        /// </summary>
        /// <param name="newRes"></param>
        public static void ChangeResolution(Point newRes)
        {
            mainScreen.Width = newRes.X;
            mainScreen.Height = newRes.Y;
            optionScreen.Width = newRes.X;
            optionScreen.Height = newRes.Y;
            GUIManager.CurrentGame.SetScreenResultion(newRes);
        }

        /// <summary>
        /// Gibt den aktuellen Spielstatus zurück.
        /// </summary>
        public static GameState CurrentState
        {
            get
            {
                return state;
            }
        }

    }
}
