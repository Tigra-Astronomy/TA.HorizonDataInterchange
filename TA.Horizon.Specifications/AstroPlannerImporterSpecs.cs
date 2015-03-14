// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonImportSpecs.cs  Last modified: 2015-03-07@16:59 by Tim Long

using System;
using System.IO;
using System.Linq;
using CommandLine;
using Machine.Specifications;
using TA.Horizon.Importers;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof (AstroPlannerImporter), "import")]
    public class when_importing_astroplanner_csv_without_light_dome
        {
        static string Filename;
        static AstroPlannerImporter Importer;
        static FileStream SourceStream;
        static HorizonData Imported;
        Establish context = () =>
            {
            Filename = @"TestData\AstroPlanner.csv";
            SourceStream = new FileStream(Filename, FileMode.Open);
            Importer = new AstroPlannerImporter(SourceStream);
            };
        Because of = () => Imported = Importer.ImportHorizon();
        It should_read_64_data_points = () => Imported.Count.ShouldEqual(72);
        // The maximum solid horizon value is 16; maximum light dome value is 38.
        It should_not_use_the_light_dome_data = () =>  Imported.Max(p=>p.Value.HorizonAltitude).ShouldBeLessThanOrEqualTo(16.0);
        }

    [Subject(typeof (AstroPlannerImporter), "Command Line")]
    public class when_importing_from_astroplanner_and_no_source_file_is_specified
        {
        static string[] CommandLineArgs;
        static AstroPlannerImporter Importer;
        static Parser Parser;
        static StringWriter HelpWriter;
        Establish context = () =>
        {
            CommandLineArgs = new[] { "--Importer", "AstroPlanner" };
            Importer = new AstroPlannerImporter();
            HelpWriter = new StringWriter();
            Parser = new Parser(with =>
            {
                with.CaseSensitive = false;
                with.IgnoreUnknownArguments = true;
                with.HelpWriter = HelpWriter;
            });
        };
        Because of = () =>
            Thrown = Catch.Exception(() => { Importer.ProcessCommandLineArguments(Parser, CommandLineArgs); });
        It should_exit_and_display_help = () => HelpWriter.ToString().ShouldNotBeEmpty();
        It should_set_the_exit_code_to_an_error = () => Environment.ExitCode.ShouldBeLessThan(0);
        It should_throw = () => Thrown.ShouldBeOfExactType<ArgumentException>();
        static Exception Thrown;
        }
    }