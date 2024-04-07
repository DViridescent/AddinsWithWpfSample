using Rhino.Commands;
using Rhino.UI;
using Rhino;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.UI.WPF.Views;
using Sample.Rhino.UI;

namespace Sample.Rhino.Entry
{
    public class ShowSamplePanelCommand : Command
    {
        public override string EnglishName => "ShowSample";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Panels.OpenPanel(typeof(MainView).GUID);
            return Result.Success;
        }
    }
}
