using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_V3
{
    public abstract class Game
    {

        private Form form;
        private PictureBox screen;
        private Timer gameTimer;
        private Graphics graphics;
        private GameTime gameTime;
        private Image drawArea;
        private Point resultion;

        /// <summary>
        /// Erstellt ein neues Spiel.
        /// </summary>
        /// <param name="form"></param>
        public Game(Form form)
        {
            this.form = form;
            MouseInput.Initialize();
            KeyBoardInput.Initialize();
            BitmapFontManager.LoadFonts();
            screen = new PictureBox();
            screen.BackColor = Color.CornflowerBlue;
            screen.Dock = DockStyle.Fill;
            gameTime = new GameTime();
            form.Controls.Add(screen);
            drawArea = new Bitmap(1280,  800);
            resultion = new Point(1280, 800);
            graphics = Graphics.FromImage(drawArea);
            GUIManager.Initialize(this);
            ContentLoader.Initialize();
        }

        /// <summary>
        /// Initialisiert das Spiel.
        /// </summary>
        public virtual void Initialize()
        {
            screen.Focus();
            screen.SizeMode = PictureBoxSizeMode.StretchImage;
            screen.MouseMove += new MouseEventHandler(OnMouseMove);
            screen.MouseDown += new MouseEventHandler(OnMouseButtonDown);
            screen.MouseUp += new MouseEventHandler(OnMouseButtonUp);
            screen.MouseWheel += new MouseEventHandler(OnMouseWheel);
            form.KeyDown += new KeyEventHandler(KeyDown);
            form.KeyUp += new KeyEventHandler(KeyUp);
            gameTimer = new Timer();
            gameTimer.Interval = 100 / 30;
            gameTimer.Tick += new EventHandler(OnTimerTick);
            gameTime.Start();
            gameTimer.Start();
        }

        /// <summary>
        /// Tritt ein, wenn das Spiel einen neuen Frame zeichnen muss.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTimerTick(object sender, EventArgs args)
        {
            graphics.Clear(Color.CornflowerBlue);
            screen.Invalidate();
            Update(gameTime);
            Draw(graphics, gameTime);
            screen.Image = drawArea;
        }

        #region MouseEvents:
        /// <summary>
        /// Tritt ein, wenn die Maus über dem Screen bewegt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            MouseInput.SetPosition(new Point(args.X, args.Y), (Bitmap)drawArea);
        }

        /// <summary>
        /// Tritt ein, wenn eine Maustaste gedrückt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseButtonDown(object sender, MouseEventArgs args)
        {
            if(args.Button == MouseButtons.Left)
            {
                MouseInput.SetButton(ButtonStates.Pressed, MouseButtonIDs.Left);
            }
            if(args.Button == MouseButtons.Middle)
            {
                MouseInput.SetButton(ButtonStates.Pressed, MouseButtonIDs.Middle);
            }
            if(args.Button == MouseButtons.Right)
            {
                MouseInput.SetButton(ButtonStates.Pressed, MouseButtonIDs.Right);
            }
        }

        /// <summary>
        /// Tritt ein, wenn eine Maustaste losgelassen wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseButtonUp(object sender, MouseEventArgs args)
        {
            if(args.Button == MouseButtons.Left)
            {
                MouseInput.SetButton(ButtonStates.Realesed, MouseButtonIDs.Left);
            }
            if(args.Button == MouseButtons.Middle)
            {
                MouseInput.SetButton(ButtonStates.Realesed, MouseButtonIDs.Middle);
            }
            if(args.Button == MouseButtons.Right)
            {
                MouseInput.SetButton(ButtonStates.Realesed, MouseButtonIDs.Right);
            }
        }

        /// <summary>
        /// Tritt ein, wenn das Mausrad bewegt wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseWheel(object sender, MouseEventArgs args)
        {
            MouseInput.SetScrollValue(args.Delta);
        }
        #endregion

        #region Key-Events:
        /// <summary>
        /// Tritt ein, wenn eine Taste gedrückt wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void KeyDown(object sender, KeyEventArgs args)
        {
            KeyBoardInput.AddPressedKey(args.KeyCode);
        }

        /// <summary>
        /// Tritt ein, wenn eine Taste losgelassen wird.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void KeyUp(object sender, KeyEventArgs args)
        {
            KeyBoardInput.RemovePressedKey(args.KeyCode);
        }
        #endregion

        protected abstract void Update(GameTime gameTime);

        protected abstract void Draw(Graphics graphics, GameTime gameTime);

        /// <summary>
        /// Überschreibt die Auflösung des Spiels.
        /// </summary>
        /// <param name="p"></param>
        public void SetScreenResultion(Point p)
        {
            resultion = p;
            drawArea = new Bitmap(resultion.X, resultion.Y);
            graphics = Graphics.FromImage(drawArea);
            GC.Collect();
        }

        /// <summary>
        /// Erzing das Spiel in den Fullscreen-Modus oder aus diesem heraus.
        /// </summary>
        /// <param name="eneable"></param>
        public void SetFullScreen(bool eneable)
        {
            if(eneable)
            {
                form.TopMost = true;
                form.WindowState = FormWindowState.Maximized;
            }
            else
            {
                form.TopMost = true;
                form.WindowState = FormWindowState.Normal;
                screen.Focus();
            }
            GC.Collect();
        }

        /// <summary>
        /// Schliesßt das Spiel.
        /// </summary>
        public void CloseGame()
        {
            form.Close();
        }

        /// <summary>
        /// Gibt das <see cref="Graphics"/>-Objekt des Screens zurück.
        /// </summary>
        protected Graphics GraphicsDevice
        {
            get
            {
                return graphics;
            }
        }

        /// <summary>
        /// Gibt die Bildschirmauflösung des Spiels zurück.
        /// </summary>
        public Point Resulution
        {
            get
            {
                return resultion;
            }
        }

    }
}
