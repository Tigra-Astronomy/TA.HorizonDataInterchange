// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AstroplannerImporter.cs  Last modified: 2015-03-07@17:20 by Tim Long

using System.IO;

namespace TA.Horizon.Importers
    {
    internal class AstroplannerImporter : IHorizonImporter
        {
        readonly Stream source;

        public AstroplannerImporter(Stream source)
            {
            this.source = source;
            }

        public HorizonData ImportHorizon()
            {
            var horizonData = new HorizonData();
            using (var reader = new StreamReader(source))
                {
                reader.ReadLine(); // Skip the header line: Azimuth,Lower,Light Dome
                while (!reader.EndOfStream)
                    {
                    var sourceLine = reader.ReadLine();
                    var parts = sourceLine.Split(',');
                    var azimuth = int.Parse(parts[0]);
                    var horizon = double.Parse(parts[1]);
                    var lightDome = double.Parse(parts[2]);
                    horizonData[azimuth] = horizon;
                    }
                }
            return horizonData;
            }
        }
    }