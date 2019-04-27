using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main.Screens
{
    public class OptionsScreen : Screen
    {

        private Label resolutionLabel;
        private ComboBox resolutionControl;

        private CheckBox borderlessControl;
        private CheckBox fullScreenControl;

        private Button applyButton;
        private Button backButton;

        private RelativeLayoutPanel relativeLayout;

        /// <summary>
        /// Erstellt einen <see cref="Screen"/> der die Spieloptionen anzeigen kann.
        /// </summary>
        public OptionsScreen()
        {
            relativeLayout = new RelativeLayoutPanel(Width, Height);
            relativeLayout.Location = new Point(0, 0);
            relativeLayout.BackgroundImage = ContentLoader.LoadImage("MainScreenBackgroundImage.jpg");
            this.AddControl(relativeLayout);

            resolutionLabel = new Label();
            resolutionLabel.Text = "Auflösung:";
            resolutionLabel.Location = new Point(150, 150);
            resolutionLabel.BackColor = Color.Transparent;
            resolutionLabel.Font.FontColor = Color.White;
            relativeLayout.AddControl(resolutionLabel);

            resolutionControl = new ComboBox();
            resolutionControl.Location = new Point(resolutionLabel.Location.X + 160, 150);
            resolutionLabel.Font.FontColor = Color.White;
            resolutionControl.BackgroundImage = ContentLoader.LoadImage("buttonBackgroundImage.jpg");
            resolutionControl.Font.FontColor = Color.White;
            resolutionControl.AddItem("800 x 600");
            resolutionControl.AddItem("1024 x 768");
            resolutionControl.AddItem("1280 x 800");
            resolutionControl.AddItem("1366 x 768");
            resolutionControl.AddItem("1440 x 900");
            resolutionControl.AddItem("1920 x 1200");
            resolutionControl.Text = "800 x 600";
            resolutionControl.BackgroundImage = ContentLoader.LoadImage("buttonBackgroundImage.jpg");
            relativeLayout.AddControl(resolutionControl);

            borderlessControl = new CheckBox();
            borderlessControl.Text = "Fensert anzeigen";
            borderlessControl.Location = new Point(resolutionControl.Location.X, 210);
            borderlessControl.BackColor = Color.Transparent;
            borderlessControl.Font.FontColor = Color.White;
            borderlessControl.Width = 300;
            relativeLayout.AddControl(borderlessControl);

            fullScreenControl = new CheckBox();
            fullScreenControl.Text = "FullScreen";
            fullScreenControl.Font.FontColor = Color.White;
            fullScreenControl.Location = new Point(resolutionControl.Location.X, 270);
            fullScreenControl.BackColor = Color.Transparent;
            relativeLayout.AddControl(fullScreenControl);

            applyButton = new Button("Bestätigen");
            applyButton.Font.FontColor = Color.White;
            applyButton.Location = new Point(150, 450);
            applyButton.Click += new MouseEventHandler(ApplyButtonClick);
            applyButton.BackgroundImage = ContentLoader.LoadImage("buttonBackgroundImage.jpg");
            relativeLayout.AddControl(applyButton);

            backButton = new Button();
            backButton.Text = "Abbrechen";
            backButton.Font.FontColor = Color.White;
            backButton.Location = new Point(380, 450);
            backButton.Click += new MouseEventHandler(BackButtonClick);
            backButton.BackgroundImage = ContentLoader.LoadImage("buttonBackgroundImage.jpg");
            relativeLayout.AddControl(backButton);
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            if(relativeLayout != null)
            {
                relativeLayout.Size = new Point(Width, Height);
            }
        }


        /// <summary>
        /// Tritt ein, wenn der Nutzer wieder in das Hauptmenü möchte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BackButtonClick(object sender, EventArgs args)
        {
            GameStateManager.ChangeState(GameState.MainMenu);
        }

        /// <summary>
        /// Tritt ein, wenn der Nutzer auf Bestätigen geklickt hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ApplyButtonClick(object sender, EventArgs args)
        {
            GUIManager.CurrentGame.SetFullScreen(fullScreenControl.Checked);
            #region ChangeResolution
            if(resolutionControl.Text == "800 x 600")
            {
                GameStateManager.ChangeResolution(new Point(800, 600));
            }
            else if(resolutionControl.Text == "1024 x 768")
            {
                GameStateManager.ChangeResolution(new Point(1024, 768));
            }
            else if(resolutionControl.Text == "1280 x 800")
            {
                GameStateManager.ChangeResolution(new Point(1280, 800));
            }
            else if(resolutionControl.Text == "1366 x 768")
            {
                GameStateManager.ChangeResolution(new Point(1366, 768));
            }
            else if(resolutionControl.Text == "1440 x 900")
            {
                GameStateManager.ChangeResolution(new Point(1440, 900));
            }
            else if(resolutionControl.Text == "1920 x 1200")
            {
                GameStateManager.ChangeResolution(new Point(1920, 1200));
            }
            #endregion
        }

    }
}
