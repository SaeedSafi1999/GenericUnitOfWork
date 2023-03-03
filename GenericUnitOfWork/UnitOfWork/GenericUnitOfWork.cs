namespace GenericUnitOfWork.UnitOfWork;

public class GenericUnitOfWork<TRepo, TEntity> :IDisposable
    where TRepo : GenericRepository<TEntity> where TEntity:class
{
    private readonly DataCotext _cotext;
    // Initialization code

    public Dictionary<Type, TRepo> repositories = new Dictionary<Type, TRepo>();

    public GenericUnitOfWork(DataCotext cotext)
    {
        _cotext = cotext;
    }

    public TRepo Repository()
    {
        if (repositories.Keys.Contains(typeof(TEntity)) == true)
        {
            return repositories[typeof(TEntity)];
        }
        TRepo repo = (TRepo)Activator.CreateInstance(
            typeof(TRepo),
            new object[] {_cotext});
        repositories.Add(typeof(TEntity), repo);
        return repo;
    }

    // other methods
    public void Dispose()
    {
        _cotext.Dispose();
    }

    public async Task<int> Save()
    {
        return await _cotext.SaveChangesAsync();
    }