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

        public async Task<IActionResult> Index()
        {
            var list = await _serviceVendedor.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _serviceDepartamento.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _serviceDepartamento.FindAllAsync();
                var viewModel = new SellerFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
            await _serviceVendedor.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        // o símbolo '?' indica que é opcional
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Informado"});
            }

            var obj = await _serviceVendedor.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Existe" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceVendedor.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Informado" }); ;
            }

            var obj = await _serviceVendedor.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Existe" }); 
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Informado" });
            }

            var obj = await _serviceVendedor.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não Existe" });
            }

            List<Departamento> departamentos = await _serviceDepartamento.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                if (!ModelState.IsValid)
                {
                    var departamentos = await _serviceDepartamento.FindAllAsync();
                    var viewModel = new SellerFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                    return View(viewModel);
                }
            }

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { messagem = "Id não diferente" });            }
            try
            {
               await _serviceVendedor.UpdateAsync(vendedor);
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
