using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_V3
{
    public class GameTime
    {

        private Timer gameTimer;
        private long seconds;
        private long milliseconds;
        private long minutes;

        /// <summary>
        /// Erstellt einen neuen GameTimer, der die Zeit misst.
        /// </summary>
        public GameTime ()
        {
            gameTimer = new Timer();
            gameTimer.Tick += new EventHandler(CountMilliseconds);
            gameTimer.Interval = 1;
        }

        /// <summary>
        /// Tritt ein, wenn die Zeit gemmesen werden muss.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CountMilliseconds(object sender, EventArgs args)
        {
            milliseconds++;
            if(milliseconds % 100 == 0 && milliseconds != 0)
            {
                seconds++;
                if(seconds % 60 == 0)
                {
                    minutes++;
                }
            }
        }

        /// <summary>
        /// Startet das Messen der Zeit des Spiels.
        /// </summary>
        public void Start()
        {
            gameTimer.Start();
        }

        /// <summary>
        /// Beendet das Messen der Zeit des Spiels.
        /// </summary>
        public void Stop()
        {
            gameTimer.Stop();
        }

        /// <summary>
        /// Löscht alle gespeicherten Zeitwerte.
        /// </summary>
        public void ClearSaves()
        {
            milliseconds = 0;
            seconds = 0;
            minutes = 0;
        }

        /// <summary>
        /// Gibt die Anzahl an Sekunden an, die das Spiel läuft.
        /// </summary>
        public long Seconds
        {
            get
            {
                return seconds;
            }
        }

        /// <summary>
        /// Gibt die Anzahl an Millisekunden an, die das Spiel bereits läuft.
        /// </summary>
        public long Milliseconds
        {
            get
            {
                return milliseconds;
            }
        }

        /// <summary>
        /// Gibt die Anzahl an Minuten an, die das Spiel läuft.
        /// </summary>
        public long Minutes
        {
            get
            {
                return minutes;
            }
        }
    }
}
