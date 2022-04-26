namespace Mongemini.Domain.Entities
{
    public abstract class EntityDomain<TKey>
    {
        public virtual TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj is not EntityDomain<TKey> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetTypes())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        private Type GetTypes()
        {
            return GetType();
        }

        public static bool operator ==(EntityDomain<TKey> a, EntityDomain<TKey> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(EntityDomain<TKey> a, EntityDomain<TKey> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
