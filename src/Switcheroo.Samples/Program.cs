namespace Switcheroo.Samples
{
    using System;
    using System.Globalization;

    class Program
    {
        static void Main(string[] args)
        {
            Feature.Initialize(x => x.FromApplicationConfig());

            var logger = new Logger();

            for (int i = 0; i< 100; i++)
            {
                logger.Log(i.ToString(CultureInfo.InvariantCulture));
            }

            Console.ReadLine();
        }
    }
}
