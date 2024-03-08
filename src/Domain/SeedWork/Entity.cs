namespace Domain.SeedWork
{
	public abstract class Entity
	{
		public long Id { get; protected set; }
		public abstract void ValidateId(long id);
		public void SetId(long id)
		{
			if (Id != 0)
				throw new Exception($"Existing entity, you can only set the Id when the object is new");
			Id = id;
		}
	}
}
