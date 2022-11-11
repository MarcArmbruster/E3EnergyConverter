namespace E3EnergyConverter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// UI value converter (used in XAML Binding).
    /// </summary>
    public class StringToDecimalConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return Parser.ToDecimal(value?.ToString());
        }

        public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}
