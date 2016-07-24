// This file is part of the TA.Horizon project
// 
// Copyright © 2015-2016 Tigra Astronomy, all rights reserved.
// 
// File: ObserverProImporterSpecs.cs Last modified: 2016-01-09@14:33 by Tim Long

using System;
using System.IO;
using System.Linq;
using CommandLine;
using Machine.Specifications;
using TA.Horizon.Importers;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof(ObserverProImporter), "import")]
    public class when_importing_ObserverPro_hzn
        {
        static string Filename;
        static ObserverProImporter Importer;
        static FileStream SourceStream;
        static HorizonData Imported;

        // TODO fix for hzn data
        static readonly int[] expectedHorizon =
            {
            13, 14, 13, 13, 13, 12, 12, 10, 10, 10, 9, 8, 10, 8, 10, 10, 10, 9, 9,
            12, 10, 12, 9, 8, 10, 14, 16, 16, 16, 15, 13, 13, 9, 8, 8, 7, 10, 11, 12, 12, 10, 8, 10, 10, 8, 6, 7, 10, 5, 9,
            9, 6, 6, 6, 6, 6, 6, 6, 10, 8, 9, 8, 6, 9, 11, 10, 12, 12, 13, 13, 14, 14
            };

        static int[] expectedLightDome =
            {
            0, 3, 11, 18, 21, 24, 26, 27, 28, 25, 29, 31, 29, 32, 32, 32, 29, 31, 31, 33,
            32, 32, 32, 32, 33, 38, 38, 37, 38, 38, 38, 35, 30, 28, 27, 21, 13, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

        Establish context = () =>
            {
            Filename = @"TestData\ObserverPro.hzn";
            SourceStream = new FileStream(Filename, FileMode.Open);
            Importer = new ObserverProImporter(SourceStream);
            };

        Because of = () => Imported = Importer.ImportHorizon();

        It should_read_data_points_every_1_degree = () =>
            {
            var index = 0;
            var keys = Imported.Keys.ToArray();
            for (var azimuth = 0; azimuth < 360; azimuth += 1)
                {
                keys[index].ShouldEqual(azimuth);
                index++;
                }
            Imported.Count.ShouldEqual(index);
            };

        It should_read_the_expected_light_dome_data = () =>
            {
            var keys = Imported.Keys.ToArray();
            for (var i = 0; i < expectedLightDome.Length; i++)
                {
                var azimuth = keys[i];
                Imported[azimuth].LightDomeAltitude.ShouldEqual(expectedLightDome[i]);
                }
            };

        It should_read_the_expected_horizon_data = () =>
            {
            var keys = Imported.Keys.ToArray();
            for (var i = 0; i < expectedHorizon.Length; i++)
                {
                var azimuth = keys[i];
                Imported[azimuth].HorizonAltitude.ShouldEqual(expectedHorizon[i]);
                }
            };
        }

    [Subject(typeof(ObserverProImporter), "Command Line")]
    public class when_importing_from_ObserverPro_and_no_source_file_is_specified
        {
        static string[] CommandLineArgs;
        static ObserverProImporter Importer;
        static Parser Parser;
        static StringWriter HelpWriter;
        static Exception Thrown;

        Establish context = () =>
            {
            CommandLineArgs = new[] {"--Importer", "ObserverPro"};
            Importer = new ObserverProImporter();
            HelpWriter = new StringWriter();
            Parser = new Parser(
                with =>
                    {
                    with.CaseSensitive = false;
                    with.IgnoreUnknownArguments = true;
                    with.HelpWriter = HelpWriter;
                    });
            };

        Because of =
            () => Thrown = Catch.Exception(() => { Importer.ProcessCommandLineArguments(Parser, CommandLineArgs); });

        It should_exit_and_display_help = () => HelpWriter.ToString().ShouldNotBeEmpty();
        It should_set_the_exit_code_to_an_error = () => Environment.ExitCode.ShouldBeLessThan(0);
        It should_throw = () => Thrown.ShouldBeOfExactType<ArgumentException>();
        }
    }