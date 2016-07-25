// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: TheSkyXImporter.cs  Last modified: 2015-03-08@01:43 by Tim Long

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using CommandLine;

namespace TA.Horizon.Importers
    {
    internal class TheSkyXImporter : IHorizonImporter
        {
        Stream source;
        string[] commandLineArguments;
        ParserResult<TheSkyXOptions> options;

        public TheSkyXImporter(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="TheSkyXImporter"/> class and directly sets the source stream.
        /// This constructor is only available internally and is intended for unit testing.
        /// </summary>
        /// <param name="source">The source.</param>
        internal TheSkyXImporter(Stream source)
            {
            this.source = source;
            }

        public HorizonData ImportHorizon()
            {
            var horizonData = new HorizonData();
            using (var reader = new StreamReader(source))
                {
                // reader.ReadLine(); // Skip the header line: Azimuth,Lower,Light Dome
                while (!reader.EndOfStream)
                    {
                    var sourceLine = reader.ReadLine();
                    var parts = sourceLine.Split(',');
                    if (parts.Length != 2) 
                        throw new FormatException("Unable to parse input file (missing fields)");
                    var azimuth = int.Parse(parts[0]);
                    var horizon = double.Parse(parts[1]);
                    horizonData[azimuth] = new HorizonDatum(horizon, 0.0);
                    }
                }
            return horizonData;
            }

        public void ProcessCommandLineArguments(Parser parser, string[] args)
            {
            this.commandLineArguments = args;
           
            options = parser.ParseArguments<TheSkyXOptions>(args);
            if (options.Errors.Any())
                {
                Environment.ExitCode = -1;
                throw new ArgumentException("An error occurred processing the command line options.");
                }

            source = new FileStream(options.Value.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
        }
    }