// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AstroplannerImporter.cs  Last modified: 2015-03-08@01:43 by Tim Long

using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace TA.Horizon.Importers
    {
    internal class AstroplannerImporter : IHorizonImporter
        {
        readonly Stream source;

        [ContractInvariantMethod]
        void ObjectInvariant()
            {
            Contract.Invariant(this.source!=null);
            }

        public AstroplannerImporter(Stream source)
            {
            Contract.Requires(source!=null);
            this.source = source;
            }

        public string SourceName
            {
            get { return "Astroplanner"; }
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
        }
    }