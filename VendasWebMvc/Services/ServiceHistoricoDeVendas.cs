using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace VendasWebMvc.Services
{
    public class ServiceHistoricoDeVendas
    {
        private readonly VendasWebMvcContext _context;

        public ServiceHistoricoDeVendas(VendasWebMvcContext context)
        {
            _context = context;
        }

        public async  Task<List<HistoricoDeVenda>> FindByDateAsync(DateTime? dataInicial,DateTime? dataFinal )
        {
            var resultado = from obj in _context.HistoricoDeVendas select obj;
            if (dataInicial.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataInicial.Value);
            }
            if (dataInicial.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataFinal.Value);
            }
            return await resultado
                // Comandos para fazer o Join  de Vendedor e Departamento
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();

        }

        public async Task<List< IGrouping<Departamento, HistoricoDeVenda> >> FindByDateGroupingAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var resultado = from obj in _context.HistoricoDeVendas select obj;
            if (dataInicial.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataInicial.Value);
            }
            if (dataInicial.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataFinal.Value);
            }
            return await resultado
                // Comandos para fazer o Join  de Vendedor e Departamento
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Vendedor.Departamento)
                .ToListAsync();

        }



    }
}
