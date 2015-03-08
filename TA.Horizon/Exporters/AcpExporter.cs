using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Exporters
    {
    class AcpExporter : IHorizonExporter
        {
        readonly IRegistryWriter writer;

        public AcpExporter(IRegistryWriter writer)
            {
            this.writer = writer;
            }

        public void ExportHorizon(HorizonData data)
            {
            var builder = new StringBuilder();
            for (int azimuth = 0; azimuth < 360; azimuth+=2)
                {
                var altitude = data[azimuth];
                builder.AppendFormat("{0:F1} ", altitude);
                }
            builder.Length--;   // Removes the trailing space
            var registryValue = builder.ToString();
            writer.SetKey("Horizon", registryValue);
            }
        }
    }
