using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Revit.Utils
{
    internal static class RevitDialog
    {
        public static void Info(string message) => Show("信息", message);
        public static void Warning(string message) => Show("警告", message);
        public static void Error(string message) => Show("错误", message);

        public static void Show(string title, string message)
        {
            TaskDialog mainDialog = new TaskDialog(title)
            {
                MainInstruction = title,
                MainContent = message,
            };
            mainDialog.Show();
        }
    }
}
