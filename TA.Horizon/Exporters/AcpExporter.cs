using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Exporters
    {
    class AcpExporter : IHorizonExporter
        {
        readonly IRegistryWriter writer;

        [UsedImplicitly]
        public AcpExporter()
            {
            this.writer = new AcpRegistryReaderWriter();
            }
        internal AcpExporter(IRegistryWriter writer)
            {
            this.writer = writer;
            }

        public void ExportHorizon(HorizonData data)
            {
            var builder = new StringBuilder();
            for (int azimuth = 0; azimuth < 360; azimuth+=2)
                {
                var altitude = data[azimuth];
                builder.AppendFormat("{0:F1} ", altitude.HorizonAltitude);
                }
            builder.Length--;   // Removes the trailing space
            writer.SetKey("Horizon", builder);
            }

        public void ProcessCommandLineArguments(string[] args)
            {
            // ACP exporter has no command line options.
            }
        }
    }
