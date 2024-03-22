using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ActionResultTool
{
    public class Response : IActionResponse
    {
        public string Message { get; set; }
        public ResponseError Error { get; set; }
    }

    public class ResponseError
    {
        public int? Code { get; set; }

        public string Message { get; set; }

    }
}
