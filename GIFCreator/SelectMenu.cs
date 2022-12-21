using Microsoft.WindowsAPICodePack.Dialogs;

namespace GIFCreator
{
    internal static class SelectMenu
    { 
        public static string FileOpenDialog(bool CheckFileExists = true)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = CheckFileExists;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return "";
        }
    }
}
