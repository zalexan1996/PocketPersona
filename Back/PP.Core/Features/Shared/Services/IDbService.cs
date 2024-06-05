namespace PP.Core.Services;

public interface IDbService
{
    public Task SaveChanges(CancellationToken cancellationToken);

    public IQueryable<T> Set<T>() where T : class;

    public void Add<T>(T entity) where T : class;
    public void Remove<T>(T entity) where T : class;
    public void AddRange<T>(IEnumerable<T> entities) where T : class;
    public void RemoveRange<T>(IEnumerable<T> entities) where T : class;
}