// This file is part of the TA.Horizon project
// 
// Copyright © 2015-2016 Tigra Astronomy, all rights reserved.
// 
// File: AcpExporterSpecs.cs Last modified: 2016-01-09@16:39 by Tim Long

using System.Linq;
using Machine.Specifications;
using Microsoft.Win32;
using TA.Horizon.Exporters;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof(AcpExporter), "export")]
    public class when_exporting_to_acp : with_horizon_0_at_north_10_at_east_5_at_south
        {
        static FakeRegistryWriter Writer;
        static AcpExporter Exporter;

        Establish context = () =>
            {
            var root = RegistryHive.ClassesRoot.ToString();
            Writer = new FakeRegistryWriter();
            Exporter = new AcpExporter(Writer);
            };

        Because of = () => Exporter.ExportHorizon(Data);
        It should_create_one_registry_key = () => Writer.RegistryKeys.Count.ShouldEqual(1);
        It should_export_180_values = () => Writer.RegistryKeys.Single().Value.Split(' ').Count().ShouldEqual(180);
        It should_name_the_registry_key_correctly = () => Writer.RegistryKeys.Keys.Single().ShouldEqual("Horizon");
        }

    [Subject(typeof(AcpExporter), "Light Dome")]
    public class when_exporting_to_acp_and_the_light_dome_option_is_set : with_horizon_0_at_north_10_at_east_5_at_south
        {
        static FakeRegistryWriter Writer;
        static AcpExporter Exporter;

        Establish context = () =>
            {
            var root = RegistryHive.ClassesRoot.ToString();
            Writer = new FakeRegistryWriter();
            Exporter = new AcpExporter(Writer);
            Exporter.options.UseLightDome = true;
            };

        Because of = () => Exporter.ExportHorizon(Data);
        It should_create_one_registry_key = () => Writer.RegistryKeys.Count.ShouldEqual(1);
        It should_export_180_values = () => RegistryValues.Count().ShouldEqual(180);

        It should_include_the_light_dome_in_the_horizon = () =>
            {
            RegistryValues[0].ShouldEqual("0.0"); // Azimuth 0°
            RegistryValues[45].ShouldEqual("14.0"); // Azimuth 90°
            RegistryValues[90].ShouldEqual("13.0"); // Azimuth 180°
            };

        It should_name_the_registry_key_correctly = () => Writer.RegistryKeys.Keys.Single().ShouldEqual("Horizon");
        static string[] RegistryValues { get { return Writer.RegistryKeys.Single().Value.Split(' '); } }
        }
    }