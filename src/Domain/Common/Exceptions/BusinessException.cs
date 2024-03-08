namespace Domain.Common.Exceptions
{
	public class BusinessException : Exception
	{
	//	public IReadOnlyCollection<ValidationError>? Errors { get; }
		public BusinessException(string? message)//, IReadOnlyCollection<ValidationError>? errors = null)
			: base(message)
		{
			//Errors = errors;
		}
	}
}
