using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_V3
{
    public partial class GameForm : Form
    {

        private GameV3 game;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            game = new GameV3(this);
            game.Initialize();
            MouseInput.SetParrentSize(this.Size.ToPoint());
        }

        /// <summary>
        /// Tritt ein, wenn die Position des Formulars geändert wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_LocationChanged(object sender, EventArgs e)
        {
            MouseInput.SetScreenPos(this.Location);
        }

        /// <summary>
        /// Tritt ein, wenn sich die Größe des Formulares ändert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_SizeChanged(object sender, EventArgs e)
        {
            MouseInput.SetParrentSize(this.Size.ToPoint());
        }
    }
}
