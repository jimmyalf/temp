using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
    public class EyeParameter : IEquatable<EyeParameter>
    {
        public virtual decimal Left { get; set; }
        public virtual decimal Right { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((EyeParameter)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (Left.GetHashCode() * 397) ^ Right.GetHashCode();
            }
        }
        public bool Equals(EyeParameter other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Left == other.Left && Right == other.Right;
        }
    }
	public class EyeParameter<TType>
	{
		public virtual TType Left { get; set; }
		public virtual TType Right { get; set; }
	}
}