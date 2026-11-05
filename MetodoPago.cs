using System;

namespace FerreteriaValdivia.Models
{
    public abstract class MetodoPago
    {
        public string Nombre { get; set; }

        protected MetodoPago(string nombre)
        {
            Nombre = nombre;
        }

        public abstract bool ProcesarPago(decimal monto);
    }

    public class PagoEfectivo : MetodoPago
    {
        public decimal MontoEntregado { get; set; }

        public PagoEfectivo(decimal montoEntregado) : base("Efectivo")
        {
            MontoEntregado = montoEntregado;
        }

        public override bool ProcesarPago(decimal monto)
        {
            Console.WriteLine($"[PAGO EFECTIVO]: Total a pagar: {monto:C}. Monto recibido: {MontoEntregado:C}.");   

            if (MontoEntregado >= monto)
            {
                decimal cambio = MontoEntregado - monto;
                Console.WriteLine($"-> Cambio exacto a entregar: {cambio:C}");
                return true;
            }

            Console.WriteLine("-> ERROR: Monto entregado insuficiente.");
            return false;
        }
    }

    public class PagoTarjeta : MetodoPago
    {
        public string NumeroTarjeta { get; set; }

        public PagoTarjeta(string numeroTarjeta) : base("Tarjeta")
        {
            NumeroTarjeta = numeroTarjeta ?? string.Empty;
        }

        public override bool ProcesarPago(decimal monto)
        {
            Console.WriteLine($"[PAGO TARJETA]: Conectando con terminal POS para cobro de {monto:C}...");

            string ultimosDigitos = NumeroTarjeta.Length >= 4 
                ? NumeroTarjeta.Substring(NumeroTarjeta.Length - 4) 
                : "****";

            Console.WriteLine($"-> Pago autorizado con tarjeta terminada en {ultimosDigitos}.");
            return true;
        }
    }
}