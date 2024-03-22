using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Sample.AutoCAD.Entry
{
    internal class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged = _canExecuteChanged;

        private static void _canExecuteChanged(object o,EventArgs e) { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is RibbonButton ribbonButton)
            {
                Application.DocumentManager.MdiActiveDocument.SendStringToExecute( (string)ribbonButton.CommandParameter, true, false, true);
            }
            else
            {
                throw new Exception("错误：未知的参数类型");
            }
        }
    }
}
