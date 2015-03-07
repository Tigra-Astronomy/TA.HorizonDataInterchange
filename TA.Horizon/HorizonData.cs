// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonData.cs  Last modified: 2015-03-07@04:34 by Tim Long

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace TA.Horizon
    {
    public class HorizonData
        {
        readonly IDictionary<int, double> values = new Dictionary<int, double>(360);
        int highest = int.MinValue;
        int lowest = int.MaxValue;
        /// <exception cref="IndexOutOfRangeException" accessor="set">Horizon index must be in the range 0..359 degrees.</exception>
        public double this[int index]
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
                values[index] = value;
                SetHighestAndLowestIndex(index);
                }
            }
        /// <summary>
        ///     Gets the number of data points in the horizon.
        /// </summary>
        [Pure]
        public int Count
            {
            get { return values.Count; }
            }

        [ContractInvariantMethod]
        void ObjectInvariant()
            {
            Contract.Invariant(values != null);
            }

        void SetHighestAndLowestIndex(int index)
            {
            if (index > highest) highest = index;
            if (index < lowest) lowest = index;
            }

        double InterpolatedHorizonValueForAzimuth(int index)
            {
            // First handle the simple case where a data point exists at the requested index; simply return it.
            if (values.ContainsKey(index)) return values[index];    
            // Complex case, we have to interpolate between the nearest higher and lower value.
            var lowerOrEqualKeys = values.Keys.Where(key => key <= index);
            var greaterOrEqualKeys = values.Keys.Where(key => key >= index);
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
                lowerKeyAzimuth = lowerKey - 360;   // results in a negative azimuth
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
            var lowerValue = values[lowerKey];
            var upperValue = values[upperKey];
            var azimuthDelta = upperKeyAzimuth - lowerKeyAzimuth; // Takes account of any 'wrap'.
            var valueDelta = upperValue - lowerValue;
            var slope = valueDelta/azimuthDelta;
            var indexOffset = index - lowerKeyAzimuth;
            var interpolatedValue = lowerValue + indexOffset*slope; // y = mx + c, equation of a straight line.
            return interpolatedValue;
            }
        }
    }