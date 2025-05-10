using CQRSDDDDataRest.Domain.Entities;
using CQRSDDDDataRest.Infrastructure.Persistence;

namespace CQRSDDDDataRest.Infrastructure.Repositories
{
    public class SqlServerPagoRepository : ISqlServerPagoRepository
    {
        private readonly SqlServerPagosDbContext _context;
        public SqlServerPagoRepository(SqlServerPagosDbContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago.Id;
        }

    }
}