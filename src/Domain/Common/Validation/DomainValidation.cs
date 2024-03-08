using Domain.Common.Exceptions;

namespace Domain.Common.Validation
{
	public class DomainValidation
	{
		public static void NotNull(object? target, string fieldName)
		{
			if (target is null)
				throw new BusinessException($"{fieldName} should not be null");
		}
        public static void NotFound(object? target, string fieldName)
        {
            if (target is null)
                throw new NotFoundException($"{fieldName} not found");
        }


        public static void NotNullOrEmpty(string? target, string fieldName)
		{
			if (String.IsNullOrWhiteSpace(target))
				throw new BusinessException($"{fieldName} should not be empty or null");
		}
		public static void MinLength(string target, int minLength, string fieldName)
		{
			if (target.Length < minLength)
				throw new BusinessException($"{fieldName} should be at least {minLength} characters long");
		}
		public static void MaxLength(string target, int maxLength, string fieldName)
		{
			if (target.Length > maxLength)
				throw new BusinessException($"{fieldName} should be less or equal {maxLength} characters long");
		}

		public static void ValidateId<TId>(TId id)
		{
			var fieldName = "Id";

			if (id is null)
				throw new NotFoundException($"{fieldName} should not be null");

			if (typeof(TId) == typeof(string))
			{
				if (String.IsNullOrWhiteSpace(id as string))
					throw new NotFoundException($"{fieldName} should not be empty or null"); 
			}
			else if (typeof(TId) == typeof(Guid))
			{
				if ((Guid)(object)id! == Guid.Empty)
					throw new NotFoundException($"{fieldName} should not be empty");
			}
			else if (typeof(TId) == typeof(int) || typeof(TId) == typeof(long) || typeof(TId) == typeof(decimal))
			{
				if (Convert.ToDecimal(id) == 0)
					throw new NotFoundException($"{fieldName} should not be zero");
			}
			else
				throw new NotFoundException($"{fieldName} should not a object");
		}
	}
}

