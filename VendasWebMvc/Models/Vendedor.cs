using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<HistoricoDeVendas> VendasRealizadas { get; set; } = new List<HistoricoDeVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, DateTime dataDeNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataDeNascimento = dataDeNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void adicionarVenda(HistoricoDeVendas venda)
        {
            VendasRealizadas.Add(venda);
        }

        public void removerVenda(HistoricoDeVendas venda)
        {
            VendasRealizadas.Remove(venda);
        }

        public double totalDeVendas(DateTime dataInicial, DateTime dataFinal)
        {
            return this.VendasRealizadas.Where(hv => hv.Data == dataInicial && hv.Data == dataFinal)
                .Sum(hv => hv.Valor);
        
        }
    }
}
