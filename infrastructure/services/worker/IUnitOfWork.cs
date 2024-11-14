namespace infrastructure.services.worker;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}