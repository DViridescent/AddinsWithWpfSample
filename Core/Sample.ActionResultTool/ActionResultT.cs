namespace Sample.ActionResultTool
{
    public sealed class ActionResult<TValue> : IActionResult where TValue : class
    {
        private readonly TValue? _value;
        public ActionResult(TValue value, string? message = null)
        {
            _value = value;
            Message = message;
            Succeeded = true;
        }

        public ActionResult(ActionResult result)
        {
            Message = result.Message;
            Succeeded = result.Succeeded;
        }

        public TValue Value => _value ?? throw new System.InvalidOperationException("Value is null");
        public bool Succeeded { get; }
        public string? Message { get; }

        public static implicit operator ActionResult<TValue>(TValue value) => new ActionResult<TValue>(value);
        public static implicit operator TValue(ActionResult<TValue> result) => result.Value;

        public static implicit operator ActionResult<TValue>(ActionResult result) => new ActionResult<TValue>(result);
        public static implicit operator ActionResult(ActionResult<TValue> result) => new ActionResult(result.Succeeded, result.Message);
    }
}
