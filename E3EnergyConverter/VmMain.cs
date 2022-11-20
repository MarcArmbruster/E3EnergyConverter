namespace E3EnergyConverter
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

        public decimal? CostsPerKWH
        {
            get => this.GetPropertyValue<decimal?>();
            set => this.SetPropertyValue<decimal?>(value, null, () => this.NotifyAllGetterProperties());
        }

        public string CostsTotal 
        {
            get => $"{(this.KiloWattHoursTotal * this.CostsPerKWH / 100)?.ToString("N2")} EUR";
        }

        public decimal? CarFactor
        {
            get => this.GetPropertyValue<decimal?>();
            set => this.SetPropertyValue<decimal?>(value, null, () => this.NotifyAllGetterProperties());
        }

        public decimal? GrammCo2
        {
            get => Converter.KiloWattHoursToGrammCo2(this.KiloWattHoursTotal, this.EnergyMix);
        }

        public string? KiloGrammCo2
        {
            get => (this.GrammCo2 / 1000)?.ToString("N1");
        }

        public string? CarKilometers
        {
            get => Converter.ToCarKilometers(this.GrammCo2, this.CarFactor).ToString("N1");
        }

        public string DumpFileName
        {
            get => this.GetPropertyValue<string>();
            set => this.SetPropertyValue(value, null, () => this.NotifyPropertyChanged(nameof(DumpFilePath)));
        }

        public string DumpFolder
        {
            get => this.GetPropertyValue<string>();
            set => this.SetPropertyValue(value, null, () => this.NotifyPropertyChanged(nameof(DumpFilePath)));
        }

        public string DumpFilePath 
        {
            get
            {
                if (this.DumpFolder == null && this.DumpFileName == null)
                {
                    return string.Empty;
                }

                return System.IO.Path.Combine(this.DumpFolder ?? string.Empty, this.DumpFileName ?? string.Empty);
            }
        }

        public VmMain()
        {
            this.ScaleFactor = 1;
            this.CostsPerKWH = 13; /* 13 Euro cent/kWh for industry usage (private: 40 Euro cent/kWh)*/
            this.MilliJoule = 0m;
            this.EnergyMix = 0m;
            this.UsageFactor = 1m;
            this.CarFactor = 150m; /* average value for used cars in germany end of 2022 */
            this.EnergyMix = 420; /* average value for germany end of 2022 */
        }

        protected override void InitCommands() 
        {
            this.CmdAgg.AddOrSetCommand(
                "DumpCommand", 
                p1 => this.CreateE3Dump(),
                p2 => System.IO.Directory.Exists(this.DumpFolder) && !string.IsNullOrEmpty(this.DumpFileName));
            this.CmdAgg.AddOrSetCommand(
                "FolderCommand",
                p1 => this.SelectFolder(),
                p2 => true);
        }

        private void CreateE3Dump()
        {
            bool success = E3DumpCreator.CreateE3Dump(DumpFormat.CSV, this.DumpFilePath);
            if (!success)
            {
                MessageBox.Show("Dump creation failed!", "E3 Dump", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show("Dump file created!", "E3 Dump", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SelectFolder()
        {           
            this.DumpFolder = Folder.Select();
        }

        private void NotifyAllGetterProperties()
        {
            this.NotifyPropertyChanged(nameof(this.MilliWattHoursAsString));
            this.NotifyPropertyChanged(nameof(this.MilliWattHoursTotal));
            this.NotifyPropertyChanged(nameof(this.KiloWattHoursTotal));
            this.NotifyPropertyChanged(nameof(this.CostsTotal));
            this.NotifyPropertyChanged(nameof(this.GrammCo2));
            this.NotifyPropertyChanged(nameof(this.KiloGrammCo2));
            this.NotifyPropertyChanged(nameof(this.CarKilometers));
        }
    }
}