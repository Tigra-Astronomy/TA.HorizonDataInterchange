// This file is part of the TA.Horizon project
// 
// Copyright © 2015 Tigra Networks., all rights reserved.
// 
// File: HorizonDatum.cs  Last modified: 2015-03-07@22:30 by Tim Long

namespace TA.Horizon
    {
    /// <summary>
    ///     Represents a measurement of the horizon and light dome altitude (in angular degrees from the mathematical horizon)
    ///     for a specific azimuth.
    /// </summary>
    public struct HorizonDatum
        {
        public HorizonDatum(double horizonAltitude, double lightDomeAltitude) : this()
            {
            HorizonAltitude = horizonAltitude;
            LightDomeAltitude = lightDomeAltitude;
            }

        public double HorizonAltitude { get; private set; }
        public double LightDomeAltitude { get; private set; }
        }
    }