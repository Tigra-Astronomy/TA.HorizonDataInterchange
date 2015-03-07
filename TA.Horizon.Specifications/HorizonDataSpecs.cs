// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonDataSpecs.cs  Last modified: 2015-03-07@05:18 by Tim Long

using System;
using Machine.Specifications;

namespace TA.Horizon.Specifications
    {
    public class with_empty_horizon
        {
        protected static HorizonData Data;
        protected static Exception Thrown;
        protected static double Value;
        Establish context = () => Data = new HorizonData();
        }
    public class with_horizon_0_at_north_10_at_east_5_at_south : with_empty_horizon
        {
        Establish context = () =>
            {
            Data[0] = 0;
            Data[90] = 10;
            Data[180] = 5;
            };
        }

    [Subject(typeof (HorizonData), "Empty dictionary")]
    public class when_getting_horizon_data_and_there_is_no_data_in_the_dictionary : with_empty_horizon
        {
        Because of = () => Thrown = Catch.Exception(() => { var result = Data[0]; });
        It should_throw = () => Thrown.ShouldBeOfExactType<InvalidOperationException>();
        }

    [Subject(typeof (HorizonData), "exact datum")]
    public class when_getting_non_interpolated_value : with_horizon_0_at_north_10_at_east_5_at_south
        {
        Because of = () => Value = Data[90];
        It should_return_an_exact_value = () => Value.ShouldEqual(10.0);
        }

    [Subject(typeof (HorizonData), "Interpolated Datum")]
    public class when_getting_an_interpolated_value_between_two_other_values : with_horizon_0_at_north_10_at_east_5_at_south
        {
        Because of = () => Value = Data[45];
        It should_return_the_interpolated_midpoint = () => Value.ShouldEqual(5.0);
        }

    [Subject(typeof (HorizonData), "Interpolated Datum with wrapping")]
    public class when_getting_and_interpolated_value_above_the_highest_index : with_horizon_0_at_north_10_at_east_5_at_south
        {
        Because of = () => Value=Data[270];
        It should_use_index_0_as_the_higher_value = () => Value.ShouldEqual(2.5);
        }
    }