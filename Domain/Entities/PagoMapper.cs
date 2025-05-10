using System.Text.Json.Serialization;

namespace CQRSDDDDataRest.Domain.Entities
{
    public static class PagoMapper
    {
        public static Pago MapearPagoDtoADominio(PagoDto dto)
        {
            if (dto == null) return null;

            if (!decimal.TryParse(dto.Monto, out var montoDecimal))
            {
                throw new ArgumentException("Monto inválido en DTO");
            }

            var monto = Monto.Crear(montoDecimal);
            var metodoPago = MetodoPago.Crear(dto.MetodoPago);

            return new Pago(
                dto.Id,
                dto.ClienteId,
                monto,
                metodoPago,
                dto.FechaPago,
                dto.Estado
            );
        }
    }
}
