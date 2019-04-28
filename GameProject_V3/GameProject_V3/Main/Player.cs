using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject_V3.Main
{
    public class Player
    {
        #region Variablen:
        string name;

        private int health;
        private int food;
        private int money;
        private int population;

        private int maxHealt;
        private int maxFood;
        private int maxPopulation;
        #endregion

        /// <summary>
        /// Erstellt einen neuen Spieler mit den Standartwerten.
        /// </summary>
        public Player(string name)
        {
            this.name = name;

            health = 20;
            food = 0;
            money = 10;
            population = 0;
            maxFood = 10;
            maxHealt = 20;
            maxPopulation = 10;
        }


        public Player(string name, int maxHealth, int health)
        {
            this.name = name;
            this.health = health;
            this.maxHealt = maxHealth;
            food = health;
            maxFood = maxHealth;
        }

        #region Eigenschaften:
        /// <summary>
        /// Gibt den Namen des Spielers zurück.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gibt die aktuelle Gesundheit des Spielers zurück.
        /// </summary>
        public int Healt
        {
            get
            {
                return health;
            }
        }

        /// <summary>
        /// Gibt das aktuelle Guthaben des Spielers zurück.
        /// </summary>
        public int Money
        {
            get
            {
                return money;
            }
        }

        /// <summary>
        /// Gibt die aktuelle Anzahl an Einwohner des Spielers zurück.
        /// </summary>
        public int Population
        {
            get
            {
                return population;
            }
        }

        /// <summary>
        /// Gibt die aktuelle Nahrung des Spielers zurück.
        /// </summary>
        public int Food
        {
            get
            {
                return food;
            }
        }

        /// <summary>
        /// Gibt die maximale Gesundheit dieses Spielers zurück.
        /// </summary>
        public int MaxHealt
        {
            get
            {
                return maxHealt;
            }
        }

        /// <summary>
        /// Gibt die maximale Menge an Nahrung an, die der Spieler besitztem kann.
        /// </summary>
        public int MaxFood
        {
            get { return maxFood; }
        }

        /// <summary>
        /// Gibt die Maximalte Anzahl an Einwohner des Spielers an.
        /// </summary>
        public int MaxPopulation
        {
            get
            {
                return maxPopulation;
            }
        }
        #endregion

    }
}
