using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Horizon.Exporters
    {
    class ConsoleExporter : IHorizonExporter
        {
        public void ExportHorizon(HorizonData data)
            {
            Console.WriteLine("Azimuth\tHorizon\tLight Dome");
            foreach (var azimuth in data)
                {
                Console.WriteLine("{0}\t{1:F1}\t{2:F1}", 
                    azimuth.Key, azimuth.Value.HorizonAltitude,
                    azimuth.Value.LightDomeAltitude);
                }
            }

        public void ProcessCommandLineArguments(string[] args)
            {
            // There are currently no command line arguments for the console exporter
            }
        }
    }
