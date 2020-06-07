using Newtonsoft.Json;

namespace Dominio.Base
{
	public abstract class EntidadeBase
	{
		[JsonProperty("id")]
		public virtual int Id { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is EntidadeBase other))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (Id < 1)
				return false;

			return Id == other.Id;
		}

		public static bool operator ==(EntidadeBase a, EntidadeBase b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(EntidadeBase a, EntidadeBase b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}