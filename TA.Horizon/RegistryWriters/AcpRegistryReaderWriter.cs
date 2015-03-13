// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AcpRegistryReaderWriter.cs  Last modified: 2015-03-12@18:05 by Tim Long

using System.ComponentModel;
using Microsoft.Win32;

namespace TA.Horizon.RegistryWriters
    {
    internal class AcpRegistryReaderWriter : IRegistryWriter, IRegistryReader
        {
        const bool ReadOnly = false;
        const bool ReadWrite = true;
        readonly string AcpObservatoryKey = @"Software\Denny\ACP\Observatory";
        static RegistryKey AcpRegistryRoot
            {
            get { return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32); }
            }

        public T GetValue<T>(string name)
            {
            using (var rootKey = AcpRegistryRoot)
                {
                using (var regKey = rootKey.OpenSubKey(AcpObservatoryKey, ReadOnly))
                    {
                    var value = regKey.GetValue(name);
                    if (value is T) 
                        return (T) value; // Don't try to convert if it is already assignable to the expected type.
                    var converter = new TypeConverter();
                    var convertedValue = converter.ConvertTo(value, typeof (T));
                    return (T) convertedValue;
                    }
                }
            }

        public void SetKey<T>(string name, T value)
            {
            using (var rootKey = AcpRegistryRoot)
                {
                using (var regKey = rootKey.OpenSubKey(AcpObservatoryKey, ReadWrite))
                    {
                    regKey.SetValue(name, value.ToString());
                    }
                }
            }
        }
    }