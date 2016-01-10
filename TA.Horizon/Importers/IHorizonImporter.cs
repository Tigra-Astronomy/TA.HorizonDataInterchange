// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: IHorizonImporter.cs  Last modified: 2015-03-08@01:43 by Tim Long

using System;
using CommandLine;

namespace TA.Horizon.Importers
    {
    internal interface IHorizonImporter
        {
        HorizonData ImportHorizon();
        /// <summary>
        /// Parse the command line arguments. Ignore any options that you don't recognise. If you can't process the
        /// options for any reason, throw an <see cref="ArgumentException"/> containing a description of the error.
        /// </summary>
        /// <param name="parser">A command line parser that you can use to parse your options.</param>
        /// <param name="args">The command line options as entered by the user.</param>
        void ProcessCommandLineArguments(Parser parser, string[] args);
        }
    }