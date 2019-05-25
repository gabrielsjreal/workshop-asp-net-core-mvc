using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace VendasWebMvc.Models
{
    public class VendasWebMvcContext : DbContext
    {
        public VendasWebMvcContext (DbContextOptions<VendasWebMvcContext> options)
            : base(options)
        {
        }

        // toda ver que criar um novo model, é importante fazer um Dbset aqui para referenciar o model com o banco
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<HistoricoDeVenda> HistoricoDeVendas { get; set; }
    }
}
