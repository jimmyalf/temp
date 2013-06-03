﻿using System;
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
        IFillTextOperation<TSource, TDestination> Fill<TType>(Expression<Func<TDestination, TType>> destination)
            where TType : TextType;
    }

    public interface IFillEntitySpecification<TSource, TDestination>
    {
        IFillEntityOperation<TSource, TDestination> FillEntity<TType>(Expression<Func<TDestination, TType>> destination);
    }

    public interface IFillTextOperation<TSource, TDestination>
    {
        IEntityCreator<TSource, TDestination> Using(Func<TSource, string> property);
    }

    public interface IFillEntityOperation<TSource, TDestination>
    {
        IEntityCreator<TSource, TDestination> Using(object item);
    }

    public class EntityFiller<TSource, TDestination>
        : IEntityCreator<TSource, TDestination>,
          IFillTextOperation<TSource, TDestination>,
          IFillEntityOperation<TSource, TDestination>
        where TDestination : new()
    {
        private readonly TSource _source;
        private readonly TDestination _destination;
        private MemberExpression _destinationProperty;

        public EntityFiller(TSource source)
        {
            _source = source;
            _destination = new TDestination();
        }

        public TDestination GetEntity()
        {
            return _destination;
        }

        public IFillTextOperation<TSource, TDestination> Fill<TType>(Expression<Func<TDestination, TType>> selector)
            where TType : TextType
        {
            _destinationProperty = (MemberExpression)selector.Body;
            return this;
        }

        public IFillEntityOperation<TSource, TDestination> FillEntity<TType>(Expression<Func<TDestination, TType>> selector)
        {
            _destinationProperty = (MemberExpression)selector.Body;
            return this;
        }

        public IEntityCreator<TSource, TDestination> Using(Func<TSource, string> selector)
        {
            var value = selector(_source);
            return Using(value);
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