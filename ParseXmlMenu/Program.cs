using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseXmlMenu
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please enter an XML path and an active path.");
                Console.WriteLine("Usage: ParseXmlMenu \"XmlPath\" \"ActivePath\"");

                return 1;
            }

            // load and parse menu
            Menu xmlMenu = new Menu();
            xmlMenu.LoadFromXml(args[0], args[1]);

            // build xml string
            string xmlMenuString = xmlMenu.ToString();

            // print it
            Console.Write(xmlMenuString);

            // wait for input before closing
            Console.WriteLine();
            Console.Write("Press any key to exit.");
            Console.ReadKey();

            return 0;
        }
    }
}