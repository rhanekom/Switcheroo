namespace Switcheroo.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Logger
    {
        #region Globals

        private Random random;
        private IList<ConsoleColor> consoleColors;

        #endregion

        #region Public Members

        public void Log(string message)
        {
            Console.ForegroundColor = GetColor();
            Console.WriteLine(message);
        }

        #endregion

        #region Private Members

        private Random Random
        {
            get
            {
                return random ?? (random = new Random((int)(DateTime.Now.Ticks % int.MaxValue)));
            }
        }

        private IList<ConsoleColor> AvailableColors
        {
            get
            {
                return consoleColors ??
                       (consoleColors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList());
            }
        }

        private ConsoleColor GetRandomColor()
        {
            return AvailableColors[Random.Next(AvailableColors.Count - 1)];
        }

        private ConsoleColor GetColor()
        {
            if (Feature.IsEnabled("Log.InColor"))
            {
                return GetRandomColor();
            }

            return ConsoleColor.White;
        }

        #endregion
    }
}
