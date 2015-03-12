using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Importers
    {
    internal class AcpImporter : IHorizonImporter
        {
        readonly IRegistryReader reader;

        public AcpImporter(IRegistryReader reader)
            {
            this.reader = reader;
            }

        public HorizonData ImportHorizon()
            {
            var results = new HorizonData();
            var horizonString = reader.GetValue<string>("Horizon");
            var altitudes = horizonString.Split(' ');
            var azimuth = 0;
            foreach (var altitudeString in altitudes)
                {
                double altitude = double.Parse(altitudeString);
                results[azimuth] = new HorizonDatum(altitude, 0);
                azimuth += 2;
                }
            return results;
            }

        public void ProcessCommandLineArguments(string[] args)
            {
            }
        }
    }
