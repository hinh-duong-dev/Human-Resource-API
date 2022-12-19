namespace Entities
{
    public class DomainEntity<T>
    {
        public T Id { get; set; }
        public DomainEntity() 
        {
        }

        public DomainEntity(T id)
        {
            Id = id;
        }

        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}
