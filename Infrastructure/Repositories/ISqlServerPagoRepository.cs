using CQRSDDDDataRest.Domain.Entities;

namespace CQRSDDDDataRest.Infrastructure.Repositories
{
    public interface ISqlServerPagoRepository
    {
        Task<int> AgregarAsync(Pago pago);
    }
}