// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: FakeRegistryReader.cs  Last modified: 2015-03-12@03:02 by Tim Long

using System.Collections.Generic;
using System.ComponentModel;

namespace TA.Horizon.RegistryWriters
    {
    internal class FakeRegistryReader : IRegistryReader
        {
        readonly IDictionary<string, string> registryValues = new Dictionary<string, string>();
        public IDictionary<string, string> FakeRegistryValues
            {
            get { return registryValues; }
            }

        public T GetValue<T>(string name)
            {
            var value = registryValues[name];
            var converter = new TypeConverter();
            var convertedValue = converter.ConvertTo(value, typeof (T));
            return (T) convertedValue;
            }
        }
    }