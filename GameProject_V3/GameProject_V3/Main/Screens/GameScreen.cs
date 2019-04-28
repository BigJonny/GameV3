using GameProject_V3.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main.Screens
{
    public class GameScreen : Screen
    {

        private CardField cardField;
        private BuildingsField buildingsField;
        private BattleGround battleGround;
        private StatsField ownerStatsfield;
        private StatsField enemyStatsField;

        /// <summary>
        /// Erstellt einen neuen GameScreen.
        /// </summary>
        public GameScreen()
        {
            this.BackgroundImage = ContentLoader.LoadImage("oldPaperTex.jpg");
            cardField = new CardField();
            cardField.Location = new Point(0, 650);
            cardField.Size = new Point(Width - 250, 800 - 650);
            this.AddControl(cardField);

            buildingsField = new BuildingsField();
            buildingsField.Location = new Point(Width - 250, 0);
            buildingsField.Size = new Point(250, Height);
            this.AddControl(buildingsField);

            battleGround = new BattleGround();
            battleGround.Location = new Point(0, 50);
            battleGround.Size = new Point(Width - 250, 550);
            this.AddControl(battleGround);

            ownerStatsfield = new StatsField(new Player("Spieler", 20, 5));
            ownerStatsfield.Location = new Point(0, 600);
            ownerStatsfield.Size = new Point(Width - 250, 50);
            this.AddControl(ownerStatsfield);

            enemyStatsField = new StatsField(new Player("Gegner", 20, 10));
            enemyStatsField.Location = new Point(0, 0);
            enemyStatsField.Size = new Point(Width - 250, 50);
            this.AddControl(enemyStatsField);
        }




    }
}
