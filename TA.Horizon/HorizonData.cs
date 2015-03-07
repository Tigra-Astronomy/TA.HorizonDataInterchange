// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonData.cs  Last modified: 2015-03-07@17:10 by Tim Long

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace TA.Horizon
    {
    public class HorizonData : Dictionary<int, HorizonDatum>
        {
        /// <exception cref="IndexOutOfRangeException" accessor="set">Horizon index must be in the range 0..359 degrees.</exception>
        public new HorizonDatum this[int index]
            {
            get
                {
                Contract.Requires<InvalidOperationException>(Count > 0,
                    "There must be at least one value in the horizon data before it can be queried");
                return InterpolatedHorizonValueForAzimuth(index);
                }
            set
                {
                if (index > 359 || index < 0)
                    throw new IndexOutOfRangeException("Horizon index must be in the range 0..359 degrees.");
                base[index] = value;
                }
            }

        HorizonDatum InterpolatedHorizonValueForAzimuth(int index)
            {
            // First handle the simple case where a data point exists at the requested index; simply return it.
            if (base.ContainsKey(index)) return base[index];
            // Complex case, we have to interpolate between the nearest higher and lower value.
            var lowerOrEqualKeys = base.Keys.Where(key => key <= index);
            var greaterOrEqualKeys = base.Keys.Where(key => key >= index);
            if (!lowerOrEqualKeys.Any() && !greaterOrEqualKeys.Any())
                throw new InvalidOperationException("Inconsistent state detected");
            // If there is a higher and lower value, then we can use them.
            // If the value doesn't exist then we must 'wrap' around the compass, as the horizon is a continuous circle.
            int lowerKey, lowerKeyAzimuth;
            if (lowerOrEqualKeys.Any())
                {
                lowerKey = lowerOrEqualKeys.Last();
                lowerKeyAzimuth = lowerKey;
                }
            else
                {
                lowerKey = greaterOrEqualKeys.Last();
                lowerKeyAzimuth = lowerKey - 360; // results in a negative azimuth
                }
            int upperKey, upperKeyAzimuth;
            if (greaterOrEqualKeys.Any())
                {
                upperKey = greaterOrEqualKeys.First();
                upperKeyAzimuth = upperKey;
                }
            else
                {
                upperKey = lowerOrEqualKeys.First();
                upperKeyAzimuth = 360 + upperKey;
                }
            var lowerValue = base[lowerKey].HorizonAltitude;
            var upperValue = base[upperKey].HorizonAltitude;
            var azimuthDelta = upperKeyAzimuth - lowerKeyAzimuth; // Takes account of any 'wrap'.
            var valueDelta = upperValue - lowerValue;
            var slope = valueDelta/azimuthDelta;
            var indexOffset = index - lowerKeyAzimuth;
            var interpolatedValue = lowerValue + indexOffset*slope; // y = mx + c, equation of a straight line.
            return new HorizonDatum(interpolatedValue, 0.0);
            }
        }
    }