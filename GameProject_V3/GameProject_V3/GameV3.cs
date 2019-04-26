using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject_V3.Controls;
using GameProject_V3.Main.Screens;

namespace GameProject_V3
{
    public class GameV3 : Game
    {

        public GameV3(System.Windows.Forms.Form form) : base(form)
        {
            ContentLoader.Initialize();
            GUIManager.SetCurrentScreen(new MainScreen());
        }



        protected override void Draw(Graphics graphics, GameTime gameTime)
        {
            GUIManager.Draw(graphics, gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            GUIManager.Update(gameTime);
        }
    }
}
