// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: IHorizonExporter.cs  Last modified: 2015-03-08@01:41 by Tim Long

using CommandLine;

namespace TA.Horizon.Exporters
    {
    public interface IHorizonExporter
        {
        void ExportHorizon(HorizonData data);
        void ProcessCommandLineArguments(Parser parser, string[] args);
        }
    }