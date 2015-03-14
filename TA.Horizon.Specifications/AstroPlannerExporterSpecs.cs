// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AstroPlannerExporterSpecs.cs  Last modified: 2015-03-14@03:21 by Tim Long

using System;
using System.IO;
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
    }