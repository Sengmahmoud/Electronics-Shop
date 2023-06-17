using AutoMapper;
using X.PagedList;

namespace Core.AutoMapper
{
    public class PagedListGenericConverter<TSource, TDestnation> : ITypeConverter<PagedList<TSource>, PagedList<TDestnation>>
    {
        public PagedList<TDestnation> Convert(PagedList<TSource> source,PagedList<TDestnation> destination, ResolutionContext context)
        {
            return new PagedList<TDestnation>(source, source.Select(m => context.Mapper.Map<TSource, TDestnation>(m)));
        }

      
    }
}