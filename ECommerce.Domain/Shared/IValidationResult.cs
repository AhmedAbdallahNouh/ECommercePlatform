namespace ECommerce.Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new Error("ValidationError", "One or more validation errors occurred.");
        Error[] Errors { get; }
    }
}
