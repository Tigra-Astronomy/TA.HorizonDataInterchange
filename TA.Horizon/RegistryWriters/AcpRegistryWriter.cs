// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AcpRegistryWriter.cs  Last modified: 2015-03-08@01:53 by Tim Long

using Microsoft.Win32;

namespace TA.Horizon.RegistryWriters
    {
    internal class AcpRegistryWriter : IRegistryWriter
        {
        readonly string AcpObservatoryKey = @"Software\Denny\ACP\Observatory";

        public void SetKey<T>(string name, T value)
            {
            using (var rootKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                using (var regKey = rootKey.OpenSubKey(AcpObservatoryKey))
                    {
                    regKey.SetValue(name, value.ToString());
                    }
                }
            }
        }
    }