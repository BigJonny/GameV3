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
            this.AddControl(optionsButton);

            leaveButton = new Button();
            leaveButton.Text = "Beenden";
            leaveButton.Location = new Point(500, 340);
            leaveButton.BackgroundImage = buttonBackground;
            leaveButton.Font.FontColor = Color.White;
            leaveButton.Click += new MouseEventHandler(OnLeaveButtonClick);
            this.AddControl(leaveButton);
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
