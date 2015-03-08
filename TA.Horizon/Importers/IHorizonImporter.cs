// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: IHorizonImporter.cs  Last modified: 2015-03-08@01:43 by Tim Long

namespace TA.Horizon.Importers
    {
    public interface IHorizonImporter
        {
        string SourceName { get; }
        HorizonData ImportHorizon();
        }
    }