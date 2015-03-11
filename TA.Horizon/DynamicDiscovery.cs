// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: DynamicDiscovery.cs  Last modified: 2015-03-10@03:58 by Tim Long

using System;
using System.Collections.Generic;
using System.Linq;
using TA.Horizon.Exporters;
using TA.Horizon.Importers;

namespace TA.Horizon
    {
    internal static class DynamicDiscovery
        {
        internal static IDictionary<string, Type> DiscoverImporters()
            {
            return DiscoverImplementorsOfInterface(typeof (IHorizonImporter));
            }

        internal static IDictionary<string, Type> DiscoverExporters()
            {
            return DiscoverImplementorsOfInterface(typeof (IHorizonExporter));
            }

        static IDictionary<string, Type> DiscoverImplementorsOfInterface(Type targetInterface)
            {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var results = new Dictionary<string, Type>();
            foreach (var assembly in assemblies)
                {
                var types = assembly.GetTypes();
                var implementingTypes = types.Where(p => p.IsClass && p.GetInterfaces().Contains(targetInterface));
                foreach (var implementingType in implementingTypes)
                    {
                    var fullName = implementingType.Name;
                    results[fullName] = implementingType;
                    }
                }
            return results;
            }
        }
    }