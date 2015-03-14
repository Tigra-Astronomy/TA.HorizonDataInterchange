// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: Program.cs  Last modified: 2015-03-10@17:59 by Tim Long

using System;
using System.Linq;
using CommandLine;
using JetBrains.Annotations;

namespace TA.Horizon
    {
    [UsedImplicitly]
    internal class Program
        {
        static void Main(string[] args)
            {
            try
                {
                var app = new HorizonApp(args);
                app.Run();
                }
            catch (Exception ex)    // Global exception handler
                {
                Console.WriteLine("Application error: {0}", ex.Message);
                if (Environment.ExitCode >= 0)
                    Environment.ExitCode = ex.HResult < 0? ex.HResult : -1;  // Unspecified error
                }
            }
        }
    }