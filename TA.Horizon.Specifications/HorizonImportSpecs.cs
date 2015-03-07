// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonImportSpecs.cs  Last modified: 2015-03-07@16:59 by Tim Long

using System.IO;
using System.Linq;
using Machine.Specifications;
using TA.Horizon.Importers;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof (AstroplannerImporter), "import")]
    public class when_importing_astroplanner_csv_without_light_dome
        {
        static string Filename;
        static AstroplannerImporter Importer;
        static FileStream SourceStream;
        static HorizonData Imported;
        Establish context = () =>
            {
            Filename = @"TestData\AstroPlanner.csv";
            SourceStream = new FileStream(Filename, FileMode.Open);
            Importer = new AstroplannerImporter(SourceStream);
            };
        Because of = () => Imported = Importer.ImportHorizon();
        It should_read_64_data_points = () => Imported.Count.ShouldEqual(72);
        // The maximum solid horizon value is 16; maximum light dome value is 38.
        It should_not_use_the_light_dome_data = () =>  Imported.Max(p=>p.Value).ShouldBeLessThanOrEqualTo(16.0);
        }
    }