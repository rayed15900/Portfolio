using Portfolio.Utility;

namespace Portfolio.BusinessLogic.Extensions
{
	public static class ValidationResultExtension
	{
		public static List<CustomValidationError> ConvertToCustomValidationError(this FluentValidation.Results.ValidationResult validationResult)
		{
			List<CustomValidationError> errors = new();
			foreach (var error in validationResult.Errors)
			{
				errors.Add(new()
				{
					PropertyName = error.PropertyName,
					ErrorMessage = error.ErrorMessage
				});
			}
			return errors;
		}
	}
}
