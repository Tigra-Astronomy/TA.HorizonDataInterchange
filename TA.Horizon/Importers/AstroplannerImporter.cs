// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AstroplannerImporter.cs  Last modified: 2015-03-08@01:43 by Tim Long

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using CommandLine;

namespace TA.Horizon.Importers
    {
    internal class AstroplannerImporter : IHorizonImporter
        {
        Stream source;
        string[] commandLineArguments;
        ParserResult<AstroplannerOptions> options;

        public AstroplannerImporter(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="AstroplannerImporter"/> class and directly sets the source stream.
        /// This constructor is only available internally and is intended for unit testing.
        /// </summary>
        /// <param name="source">The source.</param>
        internal AstroplannerImporter(Stream source)
            {
            this.source = source;
            }

        public HorizonData ImportHorizon()
            {
            //Contract.Requires<InvalidOperationException>(source!=null, "The source file was not available");
            //Contract.Requires<InvalidOperationException>(options != null, "Command line options were not successfully parsed");
            //Contract.Requires<InvalidOperationException>(!options.Errors.Any(),"There were command line errors.");
            var horizonData = new HorizonData();
            using (var reader = new StreamReader(source))
                {
                reader.ReadLine(); // Skip the header line: Azimuth,Lower,Light Dome
                while (!reader.EndOfStream)
                    {
                    var sourceLine = reader.ReadLine();
                    var parts = sourceLine.Split(',');
                    if (parts.Length != 3) 
                        throw new FormatException("Unable to parse input file (missing fields)");
                    var azimuth = int.Parse(parts[0]);
                    var horizon = double.Parse(parts[1]);
                    var lightDome = double.Parse(parts[2]);
                    horizonData[azimuth] = new HorizonDatum(horizon, lightDome);
                    }
                }
            return horizonData;
            }

        public void ProcessCommandLineArguments(string[] args)
            {
            this.commandLineArguments = args;
            var caseInsensitiveParser = new Parser(with =>
            {
                with.CaseSensitive = false;
                with.IgnoreUnknownArguments = true;
                with.HelpWriter = Console.Error;
            });
            options = caseInsensitiveParser.ParseArguments<AstroplannerOptions>(args);
            if (options.Errors.Any())
                {
                Environment.Exit(-1);
                }

            source = new FileStream(options.Value.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
        }
    }