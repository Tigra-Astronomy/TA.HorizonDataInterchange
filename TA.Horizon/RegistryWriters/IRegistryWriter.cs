namespace TA.Horizon.RegistryWriters
    {
    internal interface IRegistryWriter
        {
        void SetKey<T>(string name, T value);
        }
    }