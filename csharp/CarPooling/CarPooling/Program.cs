using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.IO;
using CarPooling.Models;
using stem.IO;

namespace CarPooling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            if (args.Count() == 0)
            {
                Console.WriteLine("No args");
                return;
            }

            var service = new CarPoolingService(new IO.InputParser(), new MatchMaker(), new Divider());

            var journeys = service.Process(args[0]).ToList();

            var outputWriter = new OutputWriter();
            outputWriter.WriteOutput("dd-out.json", journeys);
        }
    }
}
