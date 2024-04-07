using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ActionResultTool
{
    public class Response : IActionResponse
    {
        public string Message { get; set; } = string.Empty;
        public ResponseError Error { get; set; } = null!;
    }

    public class ResponseError
    {
        public int? Code { get; set; }

        public string Message { get; set; } = string.Empty;

    }
}
