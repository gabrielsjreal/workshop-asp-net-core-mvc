using System;

using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Models
{
    public class HistoricoDeVendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public StatusDaVenda Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public HistoricoDeVendas()
        {
        }

        public HistoricoDeVendas(int id, DateTime data, double valor, StatusDaVenda status, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Valor = valor;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
