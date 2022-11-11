
// smart console structure - available since .NET6 :-)
using MilliJouleConverter;

bool exit = false;

while (!exit)
{
    Console.WriteLine(string.Empty.PadRight(60, '-'));
    Console.WriteLine("### Small Energy Converter mJ -> kWh --> g CO2 ###");
    Console.WriteLine(string.Empty.PadRight(60, '-'));

    Console.Write("Your mJ value:");
    string? userEnergyInput = Console.ReadLine();
    decimal? mWh = 0m;
    decimal? co2 = 0m;
    decimal? energy = Parser.ToDecimal(userEnergyInput);
    if (energy.HasValue)
    {
        mWh = Converter.MilliJouleToMilliWattHour(energy.Value);
        Console.WriteLine($"= {mWh:N2} mWh");
    }

    Console.Write("Your local energy mix (gCO2 per kWh):");
    string? userEnergyMixInput = Console.ReadLine();
    decimal? mix = Parser.ToDecimal(userEnergyMixInput);
    if (mWh.HasValue && mix.HasValue)
    {
        co2 = Converter.MilliJouleToGrammCo2(mWh.Value, mix.Value);
        Console.WriteLine($"= {co2:N8} g CO2");
    }
   
    Console.Write("Usage factor:");
    string? usageFactorInput = Console.ReadLine();
    decimal? usageFactor = Parser.ToDecimal(usageFactorInput);
    if (usageFactor.HasValue && co2.HasValue)
    {
        Console.WriteLine($"Climate Impact: {(usageFactor*co2):N8} g CO2");
    }

    Console.Write("Exit console? (Y/N): ");
    var answer = Console.ReadLine();
    if (answer != null && answer.ToUpper().Equals("Y"))
    {
        exit = true;
    }
}