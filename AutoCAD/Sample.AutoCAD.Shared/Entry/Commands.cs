using Autodesk.AutoCAD.Runtime;
using Sample.AutoCAD.UI;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: CommandClass(typeof(Sample.AutoCAD.Entry.Commands))]
namespace Sample.AutoCAD.Entry
{
    internal class Commands
    {
        public const string SHOW_COMMAND = "ShowSample";
        //public const string ZOOM_SCALE = "ZOOM_SCALE";

        /// <summary>
        /// 显示的主窗口，暂时放在这里，后续可以移动到其他地方
        /// </summary>
        static CustomPaletteSet? _palette;
        [CommandMethod(SHOW_COMMAND)]
        public void OpenConnectorUI()
        {
            if (_palette == null)
                _palette = new CustomPaletteSet();
            _palette.Visible = true;
        }
    }
}
