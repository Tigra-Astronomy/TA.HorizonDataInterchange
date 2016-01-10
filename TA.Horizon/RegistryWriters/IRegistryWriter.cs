using System;
using System.Diagnostics.Contracts;

namespace TA.Horizon.RegistryWriters
    {
    [ContractClass(typeof (IRegistryWriterContracts))]
    internal interface IRegistryWriter
        {
        void SetKey<T>(string name, T value);
        }

    internal interface IRegistryReader
        {
        T GetValue<T>(string name);
        }

    [ContractClassFor(typeof(IRegistryWriter))]
    internal abstract class IRegistryWriterContracts : IRegistryWriter
        {
        public void SetKey<T>(string name, T value)
            {
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Requires(value != null);
            throw new System.NotImplementedException();
            }
        }
    }