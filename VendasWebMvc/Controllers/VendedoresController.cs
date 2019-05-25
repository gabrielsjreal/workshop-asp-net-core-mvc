using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Services;
using VendasWebMvc.Models;
using VendasWebMvc.Models.ViewModels;
using VendasWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly ServiceVendedor _serviceVendedor;
        private readonly ServiceDepartamento _serviceDepartamento;


        public VendedoresController(ServiceVendedor serviceVendedor, ServiceDepartamento serviceDepartamento)
        {
            _serviceVendedor = serviceVendedor;
            _serviceDepartamento = serviceDepartamento;
        }

        public IActionResult Index()
        {
            var list = _serviceVendedor.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departamentos = _serviceDepartamento.FindAll();
            var viewModel = new SellerFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View(vendedor);
            }
            _serviceVendedor.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }

        // o símbolo '?' indica que é opcional
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Informado"});
            }

            var obj = _serviceVendedor.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Existe" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _serviceVendedor.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Informado" }); ;
            }

            var obj = _serviceVendedor.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Existe" }); 
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Informado" });
            }

            var obj = _serviceVendedor.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Existe" });
            }

            List<Departamento> departamentos = _serviceDepartamento.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View(vendedor);
            }

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não diferente" });            }
            try
            {
                _serviceVendedor.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { messagem = e.Message });
            }
           
        }

        public IActionResult Error(string messagem)
        {
            var viewModel = new ErrorViewModel
            {
                Messagem = messagem,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
    }
