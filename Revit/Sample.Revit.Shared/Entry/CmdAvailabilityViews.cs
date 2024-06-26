﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Sample.Revit.Entry
{
    /// <summary>
    /// 命令可见性
    /// </summary>
    internal class CmdAvailabilityViews : IExternalCommandAvailability
    {

        /// <summary>
        /// Command Availability - Views
        /// </summary>
        /// <param name="applicationData"></param>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            // 有活动文档的时候可以显示按钮
            return applicationData?.ActiveUIDocument?.Document?.IsValidObject ?? false;
        }
    }
}
