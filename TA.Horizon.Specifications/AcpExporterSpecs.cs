// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AcpExporterSpecs.cs  Last modified: 2015-03-07@22:58 by Tim Long

using System.Linq;
using Machine.Specifications;
using Microsoft.Win32;
using TA.Horizon.Exporters;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof (AcpExporter), "export")]
    public class when_exporting_to_acp : with_horizon_0_at_north_10_at_east_5_at_south
        {
        Establish context = () =>
            {
            var root = RegistryHive.ClassesRoot.ToString();
            Writer = new FakeRegistryWriter();
            Exporter = new AcpExporter(Writer);
            };
        Because of = () => Exporter.ExportHorizon(Data) ;
        It should_create_one_registry_key = () => Writer.RegistryKeys.Count.ShouldEqual(1) ;
        It should_name_the_registry_key_correctly = () => Writer.RegistryKeys.Keys.Single().ShouldEqual("Horizon") ;
        It should_export_180_values = () => Writer.RegistryKeys.Single().Value.Split(' ').Count().ShouldEqual(180) ;
        static FakeRegistryWriter Writer;
        static AcpExporter Exporter;
        }
    }