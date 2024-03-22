using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

using Sample.ActionResultTool;
using Sample.AutoCAD.Utils;
using Sample.Core.Interfaces;
using Sample.Geometry;

namespace Sample.AutoCAD.Service
{
    internal class AutoCADGetPointService : IGetPointService
    {
        public async Task<ActionResult<Point>> GetPoint()
        {
            // 二开容易导致软件崩溃的地方需要做专门的捕获
            try
            {
                var doc = Application.DocumentManager.MdiActiveDocument;
                var ed = doc.Editor;
                Point3d point = await GetPoint(ed);
                return point.ToSampleGeometry();
            }
            catch (Exception e)
            {
                return ActionResult.FailWithMessage($"操作因为某些内部错误失败了，以下是错误信息：\n{e.Message}");
            }
        }


        private async Task<Point3d> GetPoint(Editor ed)
        {
            var promptPointOptions = new PromptPointOptions("选取定位点或")
            {
                AllowNone = true
            };
            promptPointOptions.Keywords.Add("Exception", "Exception", "老子要抛出异常(E)");

            var result = await ContextUtils.ExecuteInCommandContextAsync(() => ed.GetPoint(promptPointOptions)); // 异步包装
            if (result.Status == PromptStatus.OK)
            {
                return result.Value;
            }
            
            if (result.Status == PromptStatus.Keyword && result.StringResult == "Exception")
            {
                throw new Exception("用户选择抛出一个嚣张的异常");
            }

            throw new Exception("用户取消了操作");
        }
    }
}
