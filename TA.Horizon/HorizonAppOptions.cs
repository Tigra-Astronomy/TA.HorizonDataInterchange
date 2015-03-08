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
        [Option('s', "SourceFile")]
        public string SourceFile { get; set; }
        }
    }