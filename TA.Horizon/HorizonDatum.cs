// This file is part of the TA.Horizon project
// 
// Copyright © 2015-2016 Tigra Astronomy, all rights reserved.
// 
// File: HorizonDatum.cs Last modified: 2016-01-09@14:20 by Tim Long

namespace TA.Horizon
    {
    /// <summary>
    ///     Represents a measurement of the horizon and light dome altitude (in angular degrees from the mathematical
    ///     horizon) for a specific azimuth.
    /// </summary>
    public struct HorizonDatum
        {
        public HorizonDatum(double horizonAltitude, double lightDomeAltitude) : this()
            {
            HorizonAltitude = horizonAltitude;
            LightDomeAltitude = lightDomeAltitude;
            }

        /// <summary>
        ///     The height of the local horizon measured from the mathematical horizon at 0°
        /// </summary>
        public double HorizonAltitude { get; private set; }

        /// <summary>
        ///     The altitude of the light dome above the local horizon (not the mathematical horizon). To obtain the
        ///     absolute altitude of the light dome, simply add <see cref="HorizonAltitude" /> to
        ///     <see cref="LightDomeAltitude" />.
        /// </summary>
        public double LightDomeAltitude { get; private set; }
        }
    }