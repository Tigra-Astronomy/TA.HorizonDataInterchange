// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonAppSpecs.cs  Last modified: 2015-03-13@06:39 by Tim Long

using System;
using System.Collections.Generic;
using Machine.Specifications;
using TA.Horizon.Importers;

namespace TA.Horizon.Specifications
    {
    [Subject(typeof (HorizonApp), "Case insensitivity")]
    public class when_the_importer_is_given_in_lower_case
        {
        static Dictionary<string, Type> AllowedTypes;
        static IHorizonImporter Importer;
        Establish context = () =>
            {
            AllowedTypes = new Dictionary<string, Type>();
            AllowedTypes["AstroPlannerImporter"] = typeof (AstroPlannerImporter);
            };
        Because of =
            () =>
                Importer =
                    HorizonApp.GetInstanceOfDynamicallyDiscoveredType<IHorizonImporter>("astroplannerimPORTER",
                        AllowedTypes);
        It should_use_invariant_culture_case_insensitive_lookup =
            () => Importer.ShouldBeOfExactType<AstroPlannerImporter>();
        }

    [Subject(typeof (HorizonApp), "invalid importer")]
    public class when_the_specified_importer_is_not_in_the_list_of_discovered_importers
        {
        static Dictionary<string, Type> AllowedTypes;
        static IHorizonImporter Importer;
        static Exception Thrown;
        Establish context = () =>
            {
            AllowedTypes = new Dictionary<string, Type>();
            AllowedTypes["AcpImporter"] = typeof (AcpImporter);
            };
        Because of = () => Thrown = Catch.Exception(() =>
            Importer = HorizonApp.GetInstanceOfDynamicallyDiscoveredType<IHorizonImporter>(
                "AstroPlannerImporter", AllowedTypes));
        It should_add_the_allowed_types_to_the_exception_data_field =
            () => Thrown.Data.Keys.ShouldContain("Allowed Types");
        It should_throw_argument_exception = () => Thrown.ShouldBeOfExactType<ArgumentException>();
        }
    }