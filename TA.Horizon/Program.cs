// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: Program.cs  Last modified: 2015-03-07@00:37 by Tim Long

using System;
using CommandLine;

namespace TA.Horizon
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            var parser = new Parser();
            var options = parser.ParseArguments<HorizonAppOptions>(args);
            var app = new HorizonApp(options);
            app.Run();
            Console.ReadLine();
            }
        }
    }