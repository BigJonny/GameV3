using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main.Screens
{
    /// <summary>
    /// Ein Feld im Spiel, welches dem Spieler die eigenen Stats 
    /// oder die des Gegners anzeigen kann.
    /// </summary>
    public class StatsField : GameField
    {

        private Player currentPlayer;

        /// <summary>
        /// Erstellt ein neues Feld, welches dem Nutzer die Stats eines Spielers anzeigt.
        /// </summary>
        public StatsField(Player p)
        {
            currentPlayer = p;
            DrawBorder = true;
            BackgroundImage = ContentLoader.LoadImage("buttonBackgroundIMage.jpg");
            this.Font.FontColor = Color.White;
        }


        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);

            Font.DrawString(currentPlayer.Name, this.graphics, new Point(10, 10));

            //Zeichne Leben:
            string playerHealt = "" + currentPlayer.Healt + " / " + currentPlayer.MaxHealt;
            int lifeBarWidth = Font.MeasureString(playerHealt).X;
            this.graphics.DrawRectangle(Pens.Black, new Rectangle(100, 10, lifeBarWidth, Height - 20));
            Color lifeColor = Color.FromArgb(150, Color.DarkRed);
            int percentageWidth = (int)(PercentageValue(currentPlayer.MaxHealt, currentPlayer.Healt) * (float)lifeBarWidth);
            this.graphics.FillRectangle(new SolidBrush(lifeColor), new Rectangle(100, 10, percentageWidth, Height - 20));
            Font.DrawString(playerHealt, this.graphics, new Rectangle(100, 10, lifeBarWidth, Height - 20),
                Controls.TextAlignment.Center);

            //Zeichne Nahrung:
            string playerFood = "" + currentPlayer.Food + " / " + currentPlayer.MaxFood;
            int foodBarWidth = Font.MeasureString(playerFood).X;
            Color foodColor = Color.FromArgb(150, Color.DarkGreen);
            this.graphics.DrawRectangle(Pens.Black, new Rectangle(200, 10, foodBarWidth, Height - 20));
            int percetageFoodWidth = (int)(PercentageValue(currentPlayer.MaxFood, currentPlayer.Food) * (float)foodBarWidth);
            this.graphics.FillRectangle(new SolidBrush(foodColor), new Rectangle(200, 10, percetageFoodWidth, Height - 20));
            Font.DrawString(playerFood, this.graphics, new Point(200, 10));

            //Zeichne Population:
            string populationString = "" + currentPlayer.Population + " / " + currentPlayer.MaxPopulation;
            int popBarWidth = Font.MeasureString(populationString).X;
            Color popsColor = Color.FromArgb(150, Color.Brown);
            this.graphics.DrawRectangle(Pens.Black, new Rectangle(300, 10, popBarWidth, Height - 20));
            int percantagePopsWidth = (int)(PercentageValue(currentPlayer.MaxPopulation, currentPlayer.Population) * (float)popBarWidth);
            this.graphics.FillRectangle(new SolidBrush(popsColor), new Rectangle(300, 10, percantagePopsWidth, Height - 20));
            Font.DrawString(populationString, this.graphics, new Point(300, 10));

            Font.DrawString("Geld: " + currentPlayer.Money, this.graphics, new Point(400, 10));
            int x = Font.MeasureString("Geld: " + currentPlayer.Money).X;
            Font.DrawString("Ablagestapel: 5", this.graphics, new Point(x + 430, 10));
            int x2 = Font.MeasureString("Ablagestapel: 5").X;
            Font.DrawString("Deck: 10", this.graphics, new Point(x2 + 460 + x, 10));

            graphics.DrawImage(DrawArea, Bounds);
        }


        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
        }

        /// <summary>
        /// Gibt den prozentualen Anteil von <paramref name="percentageValue"/> an <paramref name="max"/>
        /// zurück.
        /// </summary>
        /// <param name="max"></param>
        /// <param name="percentageValue"></param>
        /// <returns></returns>
        private float PercentageValue(int max, int percentageValue)
        {
            return (float)percentageValue / (float)max;
        }

    }
}
