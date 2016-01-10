// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonApp.cs  Last modified: 2015-03-10@23:35 by Tim Long

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using CommandLine;
using TA.Horizon.Exporters;
using TA.Horizon.Importers;

namespace TA.Horizon
    {
    public class HorizonApp
        {
        const int ErrorCodeInvalidImporterOrExporter = -2;
        readonly string[] commandLineArguments;
        readonly IDictionary<string, Type> exporters = DynamicDiscovery.DiscoverExporters();
        readonly IDictionary<string, Type> importers = DynamicDiscovery.DiscoverImporters();
        ParserResult<HorizonAppOptions> options;
        readonly List<string> errorMessages = new List<string>(); 

        public HorizonApp(string[] args)
            {
            commandLineArguments = args;
            }

        public void Run()
            {
            /*
             * Here we take a first pass over the command line options and fail
             * early if there are any missing required arguments. These options determine
             * which Importer and Exporter to load. Once loaded, the Importer and Exporter
             * receive copies of the command line arguments and may perform further processing.
             * Extra arguments are allowed at this stage but may be rejected later by the
             * Importer or the Exporter.
             */
            var caseInsensitiveParser = new Parser(with =>
                {
                with.CaseSensitive = false;
                with.IgnoreUnknownArguments = true;
                with.HelpWriter = Console.Error;
                });
            try
                {
                options = caseInsensitiveParser.ParseArguments<HorizonAppOptions>(commandLineArguments);
                if (options.Errors.Any()) { errorMessages.Add("Unable to process command line arguments."); }
                }
            catch (Exception ex)
                {
                errorMessages.Add(ex.Message);
                }

            IHorizonExporter exporter = null;
            IHorizonImporter importer = null;

            // Load the specified importer and ask it to parse its command line arguments.
            try
                {
                if (!string.IsNullOrWhiteSpace(options.Value.Importer))
                    {
                    importer = GetImporter();
                    importer.ProcessCommandLineArguments(caseInsensitiveParser, commandLineArguments);
                    }
                }
            catch (Exception ex)
                {
                errorMessages.Add(ex.Message);
                }

            // Load the specified exporter and ask it to parse its command line arguments.
            try
                {
                if (!string.IsNullOrWhiteSpace(options.Value.Exporter))
                    {
                    exporter = GetExporter();
                    exporter.ProcessCommandLineArguments(caseInsensitiveParser, commandLineArguments);
                    }
                }
            catch (Exception ex)
                {
                errorMessages.Add(ex.Message);
                }

            if (importer==null || exporter==null)
                errorMessages.Add("Both an importer and an exporter must be specified.");

            if (errorMessages.Any())
                {
                foreach (var errorMessage in errorMessages) { Console.WriteLine(errorMessage); }
                DisplayImportersAndExporters();
                Environment.Exit(-1);
                }
            else
                {
                // Perform the import and the export.
                var horizon = importer.ImportHorizon();
                exporter.ExportHorizon(horizon);
                }
            }

        internal static TInstance GetInstanceOfDynamicallyDiscoveredType<TInstance>(string typeName,
            IDictionary<string, Type> allowedTypes) where TInstance : class
            {
            Contract.Requires(!string.IsNullOrEmpty(typeName));
            Contract.Requires(allowedTypes != null);
            var caseInsensitiveQuery = from allowedType in allowedTypes
                                       where allowedType.Key.Equals(typeName, StringComparison.InvariantCultureIgnoreCase)
                                       select allowedType;
            if (!caseInsensitiveQuery.Any())
                {
                var exception = new ArgumentException("The requested type is not one of the allowed types");
                exception.Data["Allowed Types"] = allowedTypes.Keys;
                throw exception;
                }
            var type = caseInsensitiveQuery.Single().Value;
            //var type = allowedTypes[typeName];
            var typeInstance = (TInstance) Activator.CreateInstance(type);
            return typeInstance;
            }

        IHorizonImporter GetImporter()
            {
            Contract.Requires(!string.IsNullOrEmpty(options.Value.Importer),
                "No importer was specified");
            var importerName = options.Value.Importer + "Importer";
            return GetInstanceOfDynamicallyDiscoveredType<IHorizonImporter>(importerName, importers);
            }

        IHorizonExporter GetExporter()
            {
            Contract.Requires(!string.IsNullOrEmpty(options.Value.Exporter),
                "No exporter was specified.");
            var exporterName = options.Value.Exporter + "Exporter";
            return GetInstanceOfDynamicallyDiscoveredType<IHorizonExporter>(exporterName, exporters);
            }

        void DisplayImportersAndExporters()
            {
            Console.WriteLine("Available importers:");
            foreach (var importer in importers.Keys) { Console.WriteLine($"\t{FriendlyName(importer)}"); }
            Console.WriteLine("Available exporters:");
            foreach (var exporter in exporters.Keys) { Console.WriteLine($"\t{FriendlyName(exporter)}"); }
            }

        string FriendlyName(string typeName)
            {
            var unqualifiedTypeName = typeName.Split('.').Last();
            var friendlyName = RemoveSuffixes(unqualifiedTypeName, "Importer", "Exporter");
            return friendlyName;
            }

        string RemoveSuffixes(string source, params string[] suffixes)
            {
            var builder = source;
            foreach (var suffix in suffixes)
                {
                var position = builder.IndexOf(suffix);
                if (position >= 0)
                    builder = builder.Remove(position);
                }
            return builder;
            }
        }
    }