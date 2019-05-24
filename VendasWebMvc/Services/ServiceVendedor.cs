using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }

        public void Insert(Vendedor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Vendedor FindById(int id)
        {
            return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }

       public void Update(Vendedor obj)
        {
            // a exclamação na frente da condição quer dizer que "Se a condição NÃO FOR VERDADEIRA"
            if(!_context.Vendedor.Any(vendedor => vendedor.Id == obj.Id))
            {
                throw new NotFoundException("Id não Encontrado");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
