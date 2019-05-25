using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Data
{
    public class PovoarDB
    {

        // comandos para o povoamento da minha base de dados
        private VendasWebMvcContext _context;

        public PovoarDB(VendasWebMvcContext context)
        {
            _context = context;
        }

        public void Enviar()
        {
            if (_context.Departamento.Any() || _context.Vendedor.Any() || _context.HistoricoDeVendas.Any())
            {
                return; // se essa consição for verdadeira, então a base de dados já está povoado
            }

            Departamento d1 = new Departamento(1, "Computadores");
            Departamento d2 = new Departamento(2, "Eletrônicos");
            Departamento d3 = new Departamento(3, "Smartphone");
            Departamento d4 = new Departamento(4, "Livros");

            Vendedor v1 = new Vendedor(1, "João", "joao@teste.com", new DateTime(1990, 01, 27), 1000.0, d1);
            Vendedor v2 = new Vendedor(2, "Pedro", "pedro@teste.com", new DateTime(1991, 12, 28), 1100.0, d2);
            Vendedor v3 = new Vendedor(3, "Maria", "joao@teste.com", new DateTime(1992, 03, 29), 1200.0, d3);
            Vendedor v4 = new Vendedor(4, "Lucas", "lucas@teste.com", new DateTime(1993, 04, 30), 1300.0, d4);
            Vendedor v5 = new Vendedor(5, "Marcos", "marcos@teste.com", new DateTime(1994, 05, 31), 1400.0, d1);
            Vendedor v6 = new Vendedor(6, "Paulo", "paulo@teste.com", new DateTime(1995, 01, 20), 1500.0, d2);

            HistoricoDeVenda h1 = new HistoricoDeVenda(1, new DateTime(2019, 05, 20), 11000.0, StatusDaVenda.Vendido, v1);
            HistoricoDeVenda h2 = new HistoricoDeVenda(2, new DateTime(2019, 06, 21), 11000.0, StatusDaVenda.Pendente, v2);
            HistoricoDeVenda h3 = new HistoricoDeVenda(3, new DateTime(2019, 07, 22), 11000.0, StatusDaVenda.Vendido, v3);
            HistoricoDeVenda h4 = new HistoricoDeVenda(4, new DateTime(2019, 08, 23), 11000.0, StatusDaVenda.Pendente, v4);
            HistoricoDeVenda h5 = new HistoricoDeVenda(5, new DateTime(2019, 09, 24), 11000.0, StatusDaVenda.Vendido, v5);
            HistoricoDeVenda h6 = new HistoricoDeVenda(6, new DateTime(2019, 10, 25), 11000.0, StatusDaVenda.Pendente, v6);
            HistoricoDeVenda h7 = new HistoricoDeVenda(7, new DateTime(2019, 11, 26), 11000.0, StatusDaVenda.Vendido, v1);
            HistoricoDeVenda h8 = new HistoricoDeVenda(8, new DateTime(2019, 12, 27), 11000.0, StatusDaVenda.Pendente, v2);
            HistoricoDeVenda h9 = new HistoricoDeVenda(9, new DateTime(2019, 01, 28), 11000.0, StatusDaVenda.Vendido, v3);
            HistoricoDeVenda h10 = new HistoricoDeVenda(10, new DateTime(2019, 02, 28), 11000.0, StatusDaVenda.Cancelado, v4);
            HistoricoDeVenda h11 = new HistoricoDeVenda(11, new DateTime(2019, 03, 30), 11000.0, StatusDaVenda.Vendido, v5);
            HistoricoDeVenda h12 = new HistoricoDeVenda(12, new DateTime(2019, 04, 11), 11000.0, StatusDaVenda.Cancelado, v6);
            HistoricoDeVenda h13 = new HistoricoDeVenda(13, new DateTime(2019, 05, 01), 11000.0, StatusDaVenda.Vendido, v1);
            HistoricoDeVenda h14 = new HistoricoDeVenda(14, new DateTime(2019, 06, 02), 11000.0, StatusDaVenda.Cancelado, v2);

            _context.Departamento.AddRange(d1,d2,d3,d4);
            _context.Vendedor.AddRange(v1, v2, v3, v4, v5, v6);
            _context.HistoricoDeVendas.AddRange(h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h4);

            _context.SaveChanges();

        }
    }
}
