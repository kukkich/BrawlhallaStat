using FluentValidation;

namespace BrawlhallaStat.Api.General.Paging;

public static class Extensions
{
    public static IQueryable<T> FromPage<T>(this IQueryable<T> source, Page page)
    {
        return source.Skip(page.GetOffset()).Take(page.Size);
    }

    public static void AddPaging(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PageValidator>();
    }
}