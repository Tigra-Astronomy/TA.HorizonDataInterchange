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
            
            }
        }
    }
