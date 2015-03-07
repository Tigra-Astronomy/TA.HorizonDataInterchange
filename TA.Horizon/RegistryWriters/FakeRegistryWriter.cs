using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Horizon.RegistryWriters
    {
    class FakeRegistryWriter : IRegistryWriter
        {
        IDictionary<string,string> registryKeys = new Dictionary<string, string>();
        public IDictionary<string,string> RegistryKeys { get {return registryKeys;} }
        public void SetKey<T>(string keyname, T value)
            {
            registryKeys[keyname] = value.ToString();
            }
        }
    }
