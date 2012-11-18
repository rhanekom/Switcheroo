namespace Switcheroo.Samples
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            Features.Initialize(x => x.FromApplicationConfig());

            DisplayFeatures();
            // new CodeFriendlyInitialization().DoIt();

            Console.ReadLine();
        }

        private static void DisplayFeatures()
        {
            Console.WriteLine("Instances :");
            Console.WriteLine("-----------");
            Console.WriteLine();

            foreach (var instance in Features.Instance.OrderBy(x => x.Name))
            {
                Console.WriteLine(instance.ToString());
                Console.WriteLine();
            }
        }

        private void LogABit()
        {
            var logger = new Logger();

            logger.Log(Features.WhatDoIHave());

            for (int i = 0; i < 100; i++)
            {
                logger.Log(i.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
