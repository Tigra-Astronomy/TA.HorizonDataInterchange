// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonApp.cs  Last modified: 2015-03-07@04:33 by Tim Long

using System;
using System.Diagnostics.Contracts;
using System.IO;
using CommandLine;
using TA.Horizon.Exporters;
using TA.Horizon.Importers;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon
    {
    public class HorizonApp
        {
        readonly ParserResult<HorizonAppOptions> options;

        public HorizonApp(ParserResult<HorizonAppOptions> options)
            {
            this.options = options;
            }

        public void Run()
            {
            //HACK - hard code importers and exporters; later, get them from command line options.
            IHorizonImporter importer = GetImporter();
            IHorizonExporter exporter = GetExporter();
            var horizon = importer.ImportHorizon();
            exporter.ExportHorizon(horizon);
            }

        IHorizonImporter GetImporter()
            {
            Contract.Requires(!string.IsNullOrEmpty(options.Value.SourceFile));
            var sourceStream = new FileStream(options.Value.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            var importer = new AstroplannerImporter(sourceStream);
            return importer;
            }

        IHorizonExporter GetExporter()
            {
            var registryWriter = new AcpRegistryWriter();
            var exporter = new AcpExporter(registryWriter);
            return exporter;
            }
        }
    }