using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;

namespace VendasWebMvc.Services
{
    public class ServiceDepartamento
    {
        private readonly VendasWebMvcContext _context;

        public ServiceDepartamento(VendasWebMvcContext context)
        {
            _context = context;
        }

        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(d => d.Nome).ToList();
        }
    }
}
