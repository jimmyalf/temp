namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders.Helpers
{
    public class TypedEntityFiller<TSource, TType, TDestination> : IFillEntityOperation<TSource, TType, TDestination>
        where TDestination : new()
    {
        private readonly SFTIEntityBuilder<TSource, TDestination> _sftiEntityBuilder;

        public TypedEntityFiller(SFTIEntityBuilder<TSource, TDestination> sftiEntityBuilder)
        {
            _sftiEntityBuilder = sftiEntityBuilder;
        }

        public IEntityCreator<TSource, TDestination> Using(TType item)
        {
            _sftiEntityBuilder.Using(item);
            return _sftiEntityBuilder;
        }
    }
}