namespace Sample.ActionResultTool
{
    public class ActionResult : IActionResult
    {
        private static readonly ActionResult _successResult = new ActionResult(true);
        private static readonly ActionResult _failResult = new ActionResult(false);

        internal ActionResult(bool succeeded, string? message = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; }
        public string? Message { get; }

        public static implicit operator bool(ActionResult result) => result.Succeeded;
        public static implicit operator ActionResult(bool succeeded) => succeeded ? Success : Fail;

        public static ActionResult SuccessWithMessage(string? message) => new ActionResult(true, message);
        public static ActionResult FailWithMessage(string? message) => new ActionResult(false, message);

        public static ActionResult Success => _successResult;
        public static ActionResult Fail => _failResult;
    }
}
