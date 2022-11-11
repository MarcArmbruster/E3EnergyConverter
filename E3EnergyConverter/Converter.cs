namespace E3EnergyConverter
{
    internal static class Converter
    {
        /// <summary>
        /// Converts mJ to mWh.
        /// </summary>
        /// <param name="mJoule">The milli Joule value.</param>
        /// <returns>The value in mWh.</returns>
        internal static decimal MilliJouleToMilliWattHour(decimal mJoule)
        {
            return mJoule / 360m * 100m;
        }

        /// <summary>
        /// Converts mWh to gCO2.
        /// </summary>
        /// <param name="mWh">Milli Watt hours value.</param>
        /// <param name="energyMix">Local energy mix in g CO2 per kWh.</param>
        /// <returns>Gramm CO2</returns>
        internal static decimal MilliWattHourToGrammCo2(decimal mWh, decimal energyMix)
        {
            if (energyMix == 0m)
            {
                return 0m;
            }
            return mWh / 1000000 / energyMix;
        }

        /// <summary>
        /// Converts mJ directly into gCO2.
        /// </summary>
        /// <param name="mJoule">Milli Joule value.</param>
        /// <param name="energyMix">Local energy mix in g CO2 per kWh.</param>
        /// <returns></returns>
        internal static decimal MilliJouleToGrammCo2(decimal mJoule, decimal energyMix)
        {
            var mWh = MilliJouleToMilliWattHour(mJoule);
            return MilliWattHourToGrammCo2(mWh, energyMix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gCO2"></param>
        /// <param name="carFactor">Factor in gCO2 per kilometer (e.q. 150 gCO2/km)</param>
        /// <returns></returns>
        internal static decimal ToCarKilometers(decimal? gCO2, decimal? carFactor)
        {
            if (gCO2 == null || carFactor == null || carFactor == 0m)
            {
                return 0m;                
            }

            return (gCO2 / carFactor).Value;
        }
    }
}
