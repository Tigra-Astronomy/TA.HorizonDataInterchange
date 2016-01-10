// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonAppOptions.cs  Last modified: 2015-03-13@05:57 by Tim Long

using CommandLine;

namespace TA.Horizon
    {
    public class HorizonAppOptions
        {
        [Option('s', "SourceFile")]
        public string SourceFile { get; set; }
        /// <summary>
        ///     Gets or sets the importer.
        /// </summary>
        /// <value>The importer.</value>
        [Option('i', "Importer", Required = true,
            HelpText =
                "Specifies the importer to be used. Other options may be required depending on the importer chosen.")]
        public string Importer { get; set; }
        [Option('e', "Exporter",
            DefaultValue = "Console",
            HelpText =
                "Specifies the exporter to be used. Other options may be required depending on the exporter chosen.")]
        public string Exporter { get; set; }
        }
    }