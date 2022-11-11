namespace E3EnergyConverter
{
    using System.Diagnostics;

    internal static class E3DumpCreator
    {
        internal static bool CreateE3Dump(DumpFormat format, string fullFilePath)
        {
            // IMPORTANT: must be run under x64 (not x86) !!!
            try
            {
                using (var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:\Windows\System32\powercfg.exe",
                        Arguments = $@"-srumutil -{format} -output {fullFilePath}",
                        UseShellExecute = true,
                        Verb = "runas",
                        ErrorDialog = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = @"C:\Windows\System32"
                    }
                })
                {
                    process.Start();
                    process.WaitForExit();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                // change with your personal exception handling
                Debug.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
