// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonApp.cs  Last modified: 2015-03-07@04:33 by Tim Long

using System;
using CommandLine;
using TA.Horizon.Importers;

namespace TA.Horizon
    {
    public class HorizonApp
        {
        public HorizonApp(ParserResult<HorizonAppOptions> options)
            {
            throw new NotImplementedException();
            }

        public void Run()
            {
            IHorizonImporter importer = GetImporter();
            }

        IHorizonImporter GetImporter()
            {
            throw new NotImplementedException();
            }
        }
    }