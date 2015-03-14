// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AstroPlannerExporterSpecs.cs  Last modified: 2015-03-14@03:21 by Tim Long

using System;
using System.IO;
using System.Linq;
using CommandLine;
using Machine.Specifications;
using TA.Horizon.Exporters;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof (AstroPlannerExporter))]
    public class when_exporting_to_astroplanner_and_no_destination_file_is_specified
        {
        static string[] CommandLineArgs;
        static AstroPlannerExporter Exporter;
        static Parser Parser;
        static StringWriter HelpWriter;
        Establish context = () =>
            {
            CommandLineArgs = new[] {"--Exporter", "AstroPlanner"};
            Exporter = new AstroPlannerExporter();
            HelpWriter = new StringWriter();
            Parser = new Parser(with =>
                {
                with.CaseSensitive = false;
                with.IgnoreUnknownArguments = true;
                with.HelpWriter = HelpWriter;
                });
            };
        Because of = () => Exporter.ProcessCommandLineArguments(Parser, CommandLineArgs);
        It should_exit_and_display_help = () => HelpWriter.ToString().ShouldNotBeEmpty();
        It should_set_the_exit_code_to_an_error = () => Environment.ExitCode.ShouldBeLessThan(0);
        }

    internal class AstroPlannerExporter : IHorizonExporter
        {
        ParserResult<AstroPlannerExporterOptions> options;

        public void ExportHorizon(HorizonData data)
            {
            throw new NotImplementedException();
            }

        public void ProcessCommandLineArguments(Parser parser, string[] args)
            {
            options = parser.ParseArguments<AstroPlannerExporterOptions>(args);
            if (options.Errors.Any())
                {
                Environment.ExitCode = -1;
                }
            }
        }

    internal class AstroPlannerExporterOptions
        {
        [Option('d', "DestinationFile", Required = true,
            HelpText = "The destination file, into which the exported data is written in CSV format.")]
        public string DestinationFile { get; set; }
        }
    }