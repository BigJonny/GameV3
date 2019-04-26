using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main.Screens
{
    public class MainScreen : Screen
    {

        private Button startButton;
        private Button optionsButton;
        private Button leaveButton;
        private Label title;

        private Bitmap buttonBackground;

        /// <summary>
        /// Erstellt einen neuen MainScreen.
        /// </summary>
        public MainScreen()
        {
            this.BackgroundImage = ContentLoader.LoadImage("MainScreenBackgroundImage.jpg");
            buttonBackground = ContentLoader.LoadImage("buttonBackgroundImage.jpg");
            startButton = new Button();
            startButton.Text = "Start";
            startButton.BackgroundImage = buttonBackground;
            startButton.Location = new Point(500, 200);
            startButton.Font.FontColor = Color.White;
            this.AddControl(startButton);

            optionsButton = new Button();
            optionsButton.Text = "Optionen";
            optionsButton.Location = new Point(500, 270);
            optionsButton.BackgroundImage = buttonBackground;
            optionsButton.Font.FontColor = Color.White;
            optionsButton.Click += new MouseEventHandler(OptionsButtonClick);

            leaveButton = new Button();
            leaveButton.Text = "Beenden";
            leaveButton.Location = new Point(500, 340);
            leaveButton.BackgroundImage = buttonBackground;
            leaveButton.Font.FontColor = Color.White;
            leaveButton.Click += new MouseEventHandler(OnLeaveButtonClick);
            this.AddControl(optionsButton);
            this.AddControl(leaveButton);

            title = new Label();
            title.Location = new Point(50, 50);
            title.Font = BitmapFontManager.GetFontFromName("Bloody");
            title.Font.FontColor = Color.DarkRed;
            title.Font.Scale = 3.0f;
            title.Text = "Knight of War";
            this.AddControl(title);
        }

        /// <summary>
        /// Tritt ein, wenn der Nutze die Optionen sehen möchte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OptionsButtonClick(object sender, MouseEventArgs args)
        {
            GameStateManager.ChangeState(GameState.OptionsMenu);
        }

        /// <summary>
        /// Tritt ein, wenn auf Beenden geklickt wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnLeaveButtonClick(object sender, MouseEventArgs args)
        {
            GUIManager.CurrentGame.CloseGame();
        }

    }
}
