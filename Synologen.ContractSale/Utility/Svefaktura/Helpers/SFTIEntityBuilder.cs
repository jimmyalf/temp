using System;
using System.Linq.Expressions;
using System.Reflection;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Helpers
{
    public interface IEntityCreator<TSource, TDestination> : IFillTextSpecification<TSource, TDestination>, IFillEntitySpecification<TSource, TDestination>
    {
        TDestination GetEntity();
    }

    public interface IFillTextSpecification<TSource, TDestination>
    {
        IFillTextOperation<TSource, TType, TDestination> Fill<TType>(Expression<Func<TDestination, TType>> destination)
            where TType : TextType, new();
    }

    public interface IFillEntitySpecification<TSource, TDestination>
    {
        IFillEntityOperation<TSource, TType, TDestination> FillEntity<TType>(Expression<Func<TDestination, TType>> destination);
    }

    public interface IFillTextOperation<TSource, TType, TDestination> where TType : TextType, new()
    {
        IEntityCreator<TSource, TDestination> Using(Func<TSource, string> property);
        IEntityCreator<TSource, TDestination> Using(Func<TSource, string> property, Func<string, string> formatter);
    }

    public interface IFillEntityOperation<TSource, in TType, TDestination>
    {
        IEntityCreator<TSource, TDestination> Using(TType item);
    }

    public class SFTIEntityBuilder<TSource, TDestination>
        : IEntityCreator<TSource, TDestination>
        where TDestination : new()
    {
        private readonly TSource _source;
        private readonly TDestination _destination;
        private MemberExpression _destinationProperty;

        public SFTIEntityBuilder(TSource source)
        {
            _source = source;
            _destination = new TDestination();
        }

        public TDestination GetEntity()
        {
            return _destination;
        }

        public IFillTextOperation<TSource, TType, TDestination> Fill<TType>(Expression<Func<TDestination, TType>> selector)
            where TType : TextType, new()
        {
            _destinationProperty = (MemberExpression)selector.Body;
            return new TypedTextFiller<TSource, TType, TDestination>(this, _source);
        }

        public IFillEntityOperation<TSource, TType, TDestination> FillEntity<TType>(Expression<Func<TDestination, TType>> selector)
        {
            _destinationProperty = (MemberExpression)selector.Body;
            return new TypedEntityFiller<TSource, TType, TDestination>(this);
        }

        public IEntityCreator<TSource, TDestination> Using(object value)
        {
            if (value != null)
            {
                var property = (PropertyInfo)_destinationProperty.Member;
                property.SetValue(_destination, value, null);
            }

            return this;
        }
    }
}