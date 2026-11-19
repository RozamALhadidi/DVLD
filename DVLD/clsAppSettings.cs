using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public static class clsAppSettings
    {
        public static readonly string AppDataFolder = Path.Combine(Application.StartupPath, "DVLD_Data");
        public static readonly string ProjectRootFolder = Directory.GetParent(Application.StartupPath).Parent.FullName;
        public static readonly string PeopleImagesFolder = Path.Combine(ProjectRootFolder, "People_Images");
        public static readonly string LogsFolder = Path.Combine(AppDataFolder, "Logs");

        public static void EnsureFoldersExist()
        {
            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            if (!Directory.Exists(ProjectRootFolder))
                Directory.CreateDirectory(ProjectRootFolder);

            if (!Directory.Exists(PeopleImagesFolder))
                Directory.CreateDirectory(PeopleImagesFolder);

            if (!Directory.Exists(LogsFolder))
                Directory.CreateDirectory(LogsFolder);
        }
        public static string GetPersonImageFullPath(string imageFileName)
        {
            if (string.IsNullOrWhiteSpace(imageFileName))
                return string.Empty;

            return Path.Combine(PeopleImagesFolder, imageFileName);
        }
    }
}
