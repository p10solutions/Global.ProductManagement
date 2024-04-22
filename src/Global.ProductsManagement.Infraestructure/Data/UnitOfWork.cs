using Global.ProductsManagement.Domain.Contracts.Data;

namespace Global.ProductsManagement.Infraestructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        readonly ProductManagementContext _context;

        public UnitOfWork(ProductManagementContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
