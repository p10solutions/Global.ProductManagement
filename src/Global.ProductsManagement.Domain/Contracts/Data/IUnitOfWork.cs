namespace Global.ProductsManagement.Domain.Contracts.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
