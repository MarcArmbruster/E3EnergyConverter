namespace E3EnergyConverter
{
    using System.IO;
    using System.Windows.Forms;

    internal static class Folder
    {
        internal static string Select()
        {
            var path = string.Empty;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }

            return path;
        }
    }
}
