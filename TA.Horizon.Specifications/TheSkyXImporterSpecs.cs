﻿// This file is part of the TA.Horizon project
// 
// Copyright © 2015-2016 Tigra Astronomy, all rights reserved.
// 
// File: TheSkyXImporterSpecs.cs Last modified: 2016-01-09@14:33 by Tim Long

using System;
using System.IO;
using System.Linq;
using CommandLine;
using Machine.Specifications;
using TA.Horizon.Importers;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof(TheSkyXImporter), "import")]
    public class when_importing_TheSkyX_hrz
        {
        static string Filename;
        static TheSkyXImporter Importer;
        static FileStream SourceStream;
        static HorizonData Imported;

        static readonly double[] expectedHorizon =
            {
            2.92, 2.92, 2.79, 2.85, 2.85, 2.72, 2.66, 2.66, 2.79, 2.79, 2.79,
 2.60, 16.71, 3.24, 3.24, 3.24, 3.50, 3.56, 3.69, 3.50, 3.56, 2.92, 3.18,
 3.11, 3.11, 3.24, 4.08, 4.08, 3.37, 3.82, 3.88, 4.01, 3.50, 3.69,
 4.08, 4.14, 3.95, 4.21, 4.14, 4.14, 2.79, 2.60, 2.66, 3.69, 4.34,
 4.14, 4.98, 5.17, 5.17, 5.43, 5.50, 5.50, 6.01, 5.82, 5.75,
 4.59, 5.11, 3.24, 3.24, 3.37, 3.56, 3.56, 4.85, 3.63, 3.63,
 3.76, 4.79, 5.50, 5.17, 5.24, 4.08, 4.21, 4.01, 4.40, 4.34,
 4.01, 3.82, 4.34, 5.30, 5.62, 6.59, 6.46, 8.01, 7.43,
 8.07, 8.20, 8.27, 8.72, 8.78, 8.91, 9.17, 9.30, 8.98,
 9.23, 9.30, 9.11, 8.07, 7.82, 9.04, 8.91, 9.17, 8.91,
 9.81, 9.43, 9.69, 9.11, 8.78, 8.98, 9.04, 9.43,
 9.36, 9.17, 10.14, 10.01, 9.56, 9.56, 10.52, 10.78,
 10.85, 10.91, 10.97, 10.97, 10.91, 10.46, 10.46, 10.52,
 10.46, 10.01, 10.78, 10.33, 9.36, 8.07, 7.56,
 8.40, 7.95, 8.59, 7.75, 8.01, 8.53, 7.88, 7.43, 8.07, 7.88, 6.98, 7.88, 7.24, 7.24, 7.43, 7.04, 7.24,
 6.72, 6.79, 7.04, 6.72, 7.11, 6.79, 6.98, 6.33, 6.72, 5.95, 5.75, 5.75, 5.43, 5.30, 5.04, 5.62, 5.17,
 4.46, 3.05, 3.56, 2.92, 3.50, 2.85, 3.30, 3.37, 3.30, 3.18, 4.01, 4.08, 4.53, 4.85, 5.17, 6.01, 6.01,
 6.01, 5.30, 4.53, 4.72, 4.46, 4.46, 4.14, 4.14, 3.88, 4.08, 4.27, 4.72, 5.11, 5.30, 5.50, 5.50, 5.17,
 5.17, 5.11, 5.11, 4.98, 4.79, 4.72, 4.79, 5.30, 5.50, 5.30, 5.75, 5.50, 5.11, 5.24, 5.17, 5.11, 5.24,
 5.24, 5.43, 5.17, 5.50, 5.43, 5.56, 5.17, 5.43, 5.43, 5.43, 5.24, 5.50, 5.17, 5.24, 5.37, 5.17, 5.17,
 4.98, 4.85, 4.21, 5.04, 5.04, 5.17, 4.66, 5.11, 5.24, 5.17, 5.30, 5.11, 5.11, 4.66, 4.72, 4.85, 4.59,
 4.53, 4.72, 4.66, 4.53, 4.72, 4.34, 4.46, 4.46, 4.21, 3.88, 3.56, 3.50, 3.24, 3.11, 3.50, 2.92, 3.63,
 2.60, 2.60, 2.72, 2.72, 3.18, 3.30, 3.24, 3.37, 3.24, 3.18, 3.18, 3.11, 2.92, 3.05, 2.72, 2.60, 2.34,
 1.69, 1.89, 1.95, 2.40, 2.21, 3.11, 2.92, 3.24, 3.24, 3.24, 3.24, 3.18, 13.75, 3.43, 3.56, 3.37, 3.24,
 3.37, 3.30, 3.37, 3.50, 3.30, 2.92, 3.37, 3.56, 3.69, 3.82, 3.76, 4.08, 3.88, 3.50, 3.30, 3.50, 3.50,
 3.50, 3.05, 3.37, 3.05, 3.24, 3.11, 3.24, 2.92, 3.50, 3.24, 3.18, 3.24, 3.24, 3.30, 3.24, 3.24, 3.43,
 3.56, 3.30, 3.18, 3.50, 3.37, 3.56, 3.18, 3.24, 3.24, 3.11, 3.18, 3.24, 3.30, 3.18, 3.24, 3.11, 3.11,
 3.05, 2.79, 2.85, 2.85, 3.05, 2.85
            };

        static int[] expectedLightDome =
            {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

        Establish context = () =>
            {
            Filename = @"TestData\TheSkyX.hrz";
            SourceStream = new FileStream(Filename, FileMode.Open);
            Importer = new TheSkyXImporter(SourceStream);
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

    [Subject(typeof(TheSkyXImporter), "Command Line")]
    public class when_importing_from_TheSkyX_and_no_source_file_is_specified
        {
        static string[] CommandLineArgs;
        static TheSkyXImporter Importer;
        static Parser Parser;
        static StringWriter HelpWriter;
        static Exception Thrown;

        Establish context = () =>
            {
            CommandLineArgs = new[] {"--Importer", "TheSkyX"};
            Importer = new TheSkyXImporter();
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