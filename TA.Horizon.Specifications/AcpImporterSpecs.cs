// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: AcpImporterSpecs.cs  Last modified: 2015-03-12@03:21 by Tim Long

using FakeItEasy;
using Machine.Specifications;
using TA.Horizon.Importers;
using TA.Horizon.RegistryWriters;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof (AcpImporter))]
    public class when_reading_acp_registry
        {
        static IRegistryReader FakeRegistry;
        static AcpImporter Importer;
        static HorizonData ImportedHorizon;
        Establish context = () =>
            {
            FakeRegistry = A.Fake<IRegistryReader>();
                A.CallTo(()=>FakeRegistry.GetValue<string>("Horizon")).Returns("13.0 13.5 14.0 14.0 13.5");
                
            Importer = new AcpImporter(FakeRegistry);
            };
        Because of = () => ImportedHorizon = Importer.ImportHorizon();
        It should_read_the_horizon_registry_value_once =
            () => A.CallTo(() => FakeRegistry.GetValue<string>("Horizon")).MustHaveHappened(Repeated.Exactly.Once);
        It should_create_the_expected_number_of_data_points = () => ImportedHorizon.Count.ShouldEqual(5);
        }
    }