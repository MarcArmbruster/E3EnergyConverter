﻿namespace E3EnergyConverter
{
    using System.Windows;
    using WpfCommandAggregator.Core;

    public class VmMain : BaseVm
    {
        public double ScaleFactor
        {
            get => this.GetPropertyValue<double>();
            set => this.SetPropertyValue(value);
        }

        public decimal? MilliJoule
        {
            get => this.GetPropertyValue<decimal?>();
            set => this.SetPropertyValue(value, null, () => this.NotifyAllGetterProperties());
        }

        public decimal? EnergyMix
        {
            get => this.GetPropertyValue<decimal?>();
            set => this.SetPropertyValue(value, null, () => this.NotifyAllGetterProperties());
        }

        public string MilliWattHoursAsString
        {
            get => $"{Converter.MilliJouleToMilliWattHour(this.MilliJoule ?? 0m).ToString("N8")} mWh";
        }

        public decimal? MilliWattHoursTotal
        {
            get => Converter.MilliJouleToMilliWattHour(this.MilliJoule ?? 0m) * UsageFactor;
        }

        public decimal? KiloWattHoursTotal
        {
            get => Converter.MilliJouleToMilliWattHour(this.MilliJoule ?? 0m) / 1000000 * UsageFactor;
        }

        public decimal? UsageFactor
        {
            get => this.GetPropertyValue<decimal?>();
            set => this.SetPropertyValue<decimal?>(value, null, () => this.NotifyAllGetterProperties());
        }

        public decimal? CarFactor
        {
            get => this.GetPropertyValue<decimal?>();
            set => this.SetPropertyValue<decimal?>(value, null, () => this.NotifyAllGetterProperties());
        }

        public decimal? MilliGrammCo2
        {
            get => this.GrammCo2 * 1000;
        }

        public decimal? GrammCo2
        {
            get => this.UsageFactor * Converter.MilliWattHourToGrammCo2(this.MilliWattHoursTotal ?? 0m, this.EnergyMix ?? 0m);
        }

        public decimal? KiloGrammCo2
        {
            get => this.UsageFactor / 1000 * Converter.MilliWattHourToGrammCo2(this.MilliWattHoursTotal ?? 0m, this.EnergyMix ?? 0m);
        }

        public decimal? CarKilometers
        {
            get => Converter.ToCarKilometers(this.GrammCo2, this.CarFactor);
        }

        public string DumpFilePath 
        {
            get => this.GetPropertyValue<string>();
            set => this.SetPropertyValue(value);
        }

        public VmMain()
        {
            this.ScaleFactor = 2;
            this.MilliJoule = 0m;
            this.EnergyMix = 0m;
            this.UsageFactor = 1m;
            this.CarFactor = 150m;
            this.EnergyMix = 420;
        }

        protected override void InitCommands() 
        {
            this.CmdAgg.AddOrSetCommand(
                "DumpCommand", 
                p1 => this.CreateE3Dump(),
                p2 => true);
        }

        private void CreateE3Dump()
        {
            bool success = E3DumpCreator.CreateE3Dump(DumpFormat.CSV, this.DumpFilePath);
            if (!success)
            {
                MessageBox.Show("Dump creation failed!");
            }
        }

        private void NotifyAllGetterProperties()
        {
            this.NotifyPropertyChanged(nameof(this.MilliWattHoursAsString));
            this.NotifyPropertyChanged(nameof(this.MilliWattHoursTotal));
            this.NotifyPropertyChanged(nameof(this.MilliGrammCo2));
            this.NotifyPropertyChanged(nameof(this.KiloWattHoursTotal));
            this.NotifyPropertyChanged(nameof(this.GrammCo2));
            this.NotifyPropertyChanged(nameof(this.KiloGrammCo2));
            this.NotifyPropertyChanged(nameof(this.CarKilometers));
        }
    }
}