using System;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders.Helpers
{
    public class TypedTextFiller<TSource, TType, TDestination> : IFillTextOperation<TSource, TType, TDestination>
        where TType : TextType, new()
        where TDestination : new()
    {
        private readonly SFTIEntityBuilder<TSource, TDestination> _sftiEntityBuilder;

        private readonly TSource _source;

        public TypedTextFiller(SFTIEntityBuilder<TSource, TDestination> sftiEntityBuilder, TSource source)
        {
            _sftiEntityBuilder = sftiEntityBuilder;
            _source = source;
        }

        public IEntityCreator<TSource, TDestination> Using(Func<TSource, string> property)
        {
            return Using(property, value => value);
        }

        public IEntityCreator<TSource, TDestination> Using(Func<TSource, string> property, Func<string, string> formatter)
        {
            var value = property(_source);

            if (!string.IsNullOrEmpty(value))
            {
                var formattedValue = formatter(value);
                var textValue = new TType { Value = formattedValue };
                _sftiEntityBuilder.Using(textValue);                
            }

            return _sftiEntityBuilder;
        }
    }
}