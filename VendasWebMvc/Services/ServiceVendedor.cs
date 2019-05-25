using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Services.Exceptions;

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

        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _context.Vendedor.ToListAsync();
        }

        public async Task InsertAsync(Vendedor obj)
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public async Task <Vendedor> FindByIdAsync(int id)
        {
            return await _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
           

            try
            {
                var obj = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

       public async Task UpdateAsync(Vendedor obj)
        {
            // a exclamação na frente da condição quer dizer que "Se a condição NÃO FOR VERDADEIRA"
            bool hasAny = await _context.Vendedor.AnyAsync(vendedor => vendedor.Id == obj.Id);
            if(! hasAny)
            {
                throw new NotFoundException("Id não Encontrado");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
