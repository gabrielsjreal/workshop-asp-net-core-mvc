using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using Microsoft.EntityFrameworkCore;
namespace VendasWebMvc.Services
{
    public class ServiceDepartamento
    {
        private readonly VendasWebMvcContext _context;

        public ServiceDepartamento(VendasWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(d => d.Nome).ToListAsync();
        }
    }
}
