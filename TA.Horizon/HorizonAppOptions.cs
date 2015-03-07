// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonAppOptions.cs  Last modified: 2015-03-07@00:35 by Tim Long

using CommandLine;

namespace TA.Horizon
    {
    public class HorizonAppOptions
        {
        [Option("Source", Required = true, HelpText = "Specifies which program is to supply the source data")]
        public string Source { get; set; }
        [Option("IncludeLightDome",
            HelpText =
                "Includes the light dome as part of the solid horizon, if light dome data is present. Ignored if there is no light dome data."
            )]
        public bool IncludeLightDome { get; set; }
        [Option('s', "SourceFile")]
        public string SourceFile { get; set; }
        }
    }