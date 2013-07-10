namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders.Helpers
{
    public class BuildSourceContext<TDestination> where TDestination : new()
    {
        public SFTIEntityBuilder<TSource, TDestination> With<TSource>(TSource source)
        {
            return new SFTIEntityBuilder<TSource, TDestination>(source);
        }
    }
}