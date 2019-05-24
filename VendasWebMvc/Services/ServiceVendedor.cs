using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;

namespace VendasWebMvc.Services
{
    public class ServiceVendedor
    {
        private readonly VendasWebMvcContext _context;

        public ServiceVendedor(VendasWebMvcContext context)
        {
            _context = context;
        }

        // Comando para retornar uma lista com todos os vendedores do banco de Dados

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }
    }
}
