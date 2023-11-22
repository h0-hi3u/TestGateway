using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IQueryable<T> Where<T> (this Repository<T> repository, Expression<Func<T, bool>> predicate) where T : class
        => repository.Entities.Where(predicate);

    public static async Task<List<T>> ToListAsync<T>(this Repository<T> repository) where T : class
       => await repository.ToListAsync();

    public static async Task<List<T>> ToListAsync<T>(this Repository<T> repository, Expression<Func<T, bool>> predicate) where T : class
       => await repository
            .Where(predicate)
            .ToListAsync();
}
