/*
The MIT License

Copyright (c) 2013 Riaan Hanekom

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

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
