
using System.Collections.Generic;
using System;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
           
        }

        public void adicionarVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public double totalDeVendasDoVendedor(DateTime dataInicial, DateTime dataFinal)
        {
            return Vendedores.Sum(vendedor => vendedor.totalDeVendas(dataInicial, dataFinal));
        }
    }
}
