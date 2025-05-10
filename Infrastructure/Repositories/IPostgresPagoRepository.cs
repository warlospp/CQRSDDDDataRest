using CQRSDDDDataRest.Domain.Entities;

namespace CQRSDDDDataRest.Infrastructure.Repositories
{
    public interface IPostgresPagoRepository
    {
        Task<int> AgregarAsync(Pago pago);
    }
}