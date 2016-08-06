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

        static readonly double[] expectedHorizon =
    {
            11.45, 11.38, 11.55, 12.24, 12.91, 13.10, 13.47, 14.08, 14.93, 15.06, 14.77, 14.74, 14.86, 14.67, 14.47, 14.58, 14.82, 14.81, 14.68,
 14.56, 15.06, 14.87, 15.32, 15.18, 15.07, 14.77, 14.56, 12.25, 11.73, 11.27, 10.93, 10.65, 10.43, 10.37, 9.62, 9.00, 8.79, 8.37, 8.09,
 8.13, 8.10, 7.81, 7.44, 7.02, 6.21, 6.24, 5.47, 5.24, 5.01, 6.19, 5.78, 5.71, 5.45, 4.41, 4.59, 4.71, 4.57, 4.83, 5.76, 5.54, 5.63,
 5.21, 7.42, 9.50, 9.18, 8.37, 7.95, 7.26, 6.45, 6.02, 5.76, 5.14, 5.38, 5.30, 5.23, 5.13, 5.12, 5.22, 5.28, 5.29, 5.47, 5.50, 5.90,
 6.11, 6.16, 6.35, 6.75, 7.27, 7.74, 8.62, 10.39, 11.39, 13.97, 15.55, 16.83, 17.21, 17.40, 17.25, 16.36, 16.62, 16.46, 16.55, 16.83,
 16.77, 17.10, 17.52, 18.15, 18.49, 18.64, 18.80, 18.91, 18.96, 19.23, 19.56, 20.36, 20.71, 21.14, 21.51, 21.81, 21.71, 21.60, 21.39,
 20.56, 20.17, 19.40, 18.34, 17.40, 17.45, 17.54, 17.47, 17.48, 17.49, 17.58, 18.34, 18.83, 19.50, 19.84, 20.90, 22.08, 23.27, 23.64,
 24.07, 24.65, 25.45, 26.49, 27.24, 27.18, 28.08, 29.45, 29.55, 29.82, 29.80, 29.90, 29.80, 29.70, 29.63, 29.25, 29.27, 29.36, 29.53,
 29.41, 29.29, 29.66, 29.70, 29.60, 29.17, 28.45, 25.89, 25.42, 24.36, 23.74, 23.91, 24.98, 24.43, 24.89, 25.21, 25.48, 25.42, 25.29,
 24.40, 22.42, 22.06, 22.26, 23.38, 23.95, 23.78, 23.65, 23.36, 22.57, 21.85, 21.28, 21.04, 21.58, 22.45, 21.94, 21.62, 21.78, 21.72,
 21.94, 22.08, 22.15, 22.22, 23.07, 23.51, 23.98, 24.65, 24.93, 25.10, 24.99, 24.79, 28.79, 29.74, 30.15, 30.32, 30.37, 30.32, 30.20,
 30.39, 30.34, 30.16, 30.02, 30.14, 30.05, 29.73, 29.47, 29.11, 28.66, 28.19, 27.79, 27.43, 26.84, 26.49, 25.99, 25.39, 25.16, 24.73,
 24.35, 23.85, 22.78, 22.28, 21.26, 20.16, 18.13, 16.13, 15.24, 14.83, 14.92, 14.98, 15.11, 15.19, 15.37, 15.40, 15.65, 15.71, 15.89,
 17.26, 20.39, 21.13, 21.44, 24.77, 26.47, 27.79, 28.24, 28.75, 29.23, 29.75, 29.98, 30.33, 30.20, 29.93, 29.44, 28.00, 27.22, 19.43,
 18.80, 18.26, 17.95, 18.08, 21.43, 22.26, 24.47, 24.49, 24.32, 23.98, 23.36, 22.48, 22.26, 22.51, 22.96, 23.58, 23.67, 23.62, 23.70,
 24.24, 23.33, 22.46, 22.19, 22.36, 21.64, 21.24, 17.75, 17.65, 17.66, 17.56, 16.37, 15.99, 15.56, 15.36, 14.71, 14.01, 12.88, 12.51,
 12.61, 12.68, 12.69, 12.41, 12.55, 12.54, 12.66, 13.30, 13.77, 13.34, 12.78, 12.54, 5.63, 5.76, 6.05, 6.33, 6.97, 6.76, 7.11, 7.50,
 8.20, 7.22, 9.15, 9.41, 10.50, 11.84, 12.54, 12.82, 13.38, 13.73, 14.12, 14.01, 13.73, 15.64, 15.50, 14.04, 13.36, 13.31, 12.81,
 12.40, 12.30, 12.62, 12.28, 12.20, 12.11, 12.25, 12.02, 11.81
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