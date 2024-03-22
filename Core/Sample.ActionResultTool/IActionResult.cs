namespace Sample.ActionResultTool
{
    public interface IActionResult
    {
        bool Succeeded { get; }
        string? Message { get; }
    }
}