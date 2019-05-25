using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class HistoricoDeVendasController : Controller
    {
        private readonly ServiceHistoricoDeVendas _serviceHistoricoDeVendas;

        public HistoricoDeVendasController(ServiceHistoricoDeVendas serviceHistoricoDeVendas)
        {
            _serviceHistoricoDeVendas = serviceHistoricoDeVendas;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PesquisaSimples(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue)
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataFinal.HasValue)
            {
                dataFinal = DateTime.Now;
            }
            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");
            var result = await _serviceHistoricoDeVendas.FindByDateAsync(dataInicial, dataFinal);
            return View(result);
        }




        public async Task<IActionResult> PesquisaComposta(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue)
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataFinal.HasValue)
            {
                dataFinal = DateTime.Now;
            }
            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");
            var result = await _serviceHistoricoDeVendas.FindByDateGroupingAsync(dataInicial, dataFinal);
            return View(result);
        }
    }
}