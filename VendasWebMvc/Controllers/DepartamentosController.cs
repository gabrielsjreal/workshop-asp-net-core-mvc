using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Models;

namespace VendasWebMvc.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> list = new List<Departamento>();
            list.Add(new Departamento { Id = 1, Nome = "Eletônicos" });
            list.Add(new Departamento { Id = 2, Nome = "Fashion" });

            return View(list);
        }
    }
}