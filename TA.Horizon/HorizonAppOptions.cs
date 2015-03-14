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
            Required = true,
            DefaultValue = "Console",
            HelpText =
                "Specifies the exporter to be used. Other options may be required depending on the exporter chosen.")]
        public string Exporter { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether light dome data (if present) should be used
        ///     when exporting the horizon. This is useful for sources such as AstroPlanner, which include
        ///     light dome data in addition to the solid horizon, which is then exported to a program such as
        ///     ACP, which has no concept of light dome. If ACP is to be configured to avoid the light dome,
        ///     then setting this option will cause the exported horizon to use the light dome shape, if it is
        ///     higher than the solid horizon.
        /// </summary>
        /// <value><c>true</c> if [use light dome]; otherwise, <c>false</c>.</value>
        [Option('l', "UseLightDome", DefaultValue = false,
            HelpText = "Includes light dome data (if present) in the exported horizon.")]
        public bool UseLightDome { get; set; }
        }
    }