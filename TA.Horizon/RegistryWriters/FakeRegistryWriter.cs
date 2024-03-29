﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Horizon.RegistryWriters
    {
    class FakeRegistryWriter : IRegistryWriter
        {
        readonly IDictionary<string,string> registryKeys = new Dictionary<string, string>();
        public IDictionary<string,string> RegistryKeys { get {return registryKeys;} }
        public void SetKey<T>(string name, T value)
            {
            registryKeys[name] = value.ToString();
            }
        }
    }
