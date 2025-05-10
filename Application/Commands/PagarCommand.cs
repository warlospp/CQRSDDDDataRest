namespace CQRSDDDDataRest.Application.Commands
{
    public class PagarCommand 
    {
        public required string ClienteId { get; set; }
        public decimal Monto { get; set; }
        public required string MetodoPago { get; set; }
    }
}
