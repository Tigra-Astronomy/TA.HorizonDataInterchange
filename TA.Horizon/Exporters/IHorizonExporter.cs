// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: IHorizonExporter.cs  Last modified: 2015-03-08@01:41 by Tim Long

namespace TA.Horizon.Exporters
    {
    public interface IHorizonExporter
        {
        string Name { get; }
        void ExportHorizon(HorizonData data);
        }
    }