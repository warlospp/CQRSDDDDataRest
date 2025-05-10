using CQRSDDDDataRest.Domain.Entities;
using CQRSDDDDataRest.Infrastructure.Persistence;

namespace CQRSDDDDataRest.Infrastructure.Repositories
{
    public class PostgresPagoRepository : IPostgresPagoRepository
    {
        private readonly PostgresPagosDbContext _context;
        public PostgresPagoRepository(PostgresPagosDbContext context)
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
