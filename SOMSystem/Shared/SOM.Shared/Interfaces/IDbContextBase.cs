namespace SOM.Shared.Interfaces
{
    public interface IDbContextBase : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}
