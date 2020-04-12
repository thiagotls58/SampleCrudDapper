using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Configuration;
using SampleCrudDapper.Models;
using SampleCrudDapper.Repository;
using System;

namespace SampleCrudDapper.Controllers
{
    public class ProdutosController : Controller
    {
        IConfiguration _configuration;
        public ProdutosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View(new ProdutosRepository(_configuration).GetAll());
        }

        // GET
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Nome, Estoque, Preco")] Produtos produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new ProdutosRepository(_configuration).Add(produto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados");
            }
            return View(produto);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            
            Produtos produto = new ProdutosRepository(_configuration).Get(id.Value);
            if (produto == null)
                return NotFound();
            return View(produto);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Produtos produto = new ProdutosRepository(_configuration).Get(id.Value);
            if (produto == null)
                return NotFound();
            return View(produto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ProdutoId, Nome, Estoque, Preco")] Produtos produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new ProdutosRepository(_configuration).Edit(produto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados");
            }
            return View(produto);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            new ProdutosRepository(_configuration).Delete(id.Value);
            return RedirectToAction(nameof(Index));
        }
    }
}