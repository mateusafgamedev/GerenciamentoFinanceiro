using GerenciamentoFinanceiro.Data;
using GerenciamentoFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GerenciamentoFinanceiro.Controllers
{
    public class HomeController : Controller
    { 
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string id)
        {

            var filtros = new Filtros(id);

            ViewBag.Filtros = filtros;
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Transacoes = _context.Transacoes.ToList();
            ViewBag.DataOperacao = Filtros.ObterDataOperacao();

            IQueryable<Financeiro> consulta = _context.Financas
                .Include(t => t.Transacao)
                .Include(c => c.Categoria);

            if (filtros.TemCategoria)
            {
                consulta = consulta.Where(f => f.CategoriaId == filtros.CategoriaId);
            };

            if (filtros.TemTransacao)
            {
                consulta = consulta.Where(f => f.TransacaoId == filtros.TransacaoId);
            };

            if (filtros.TemDataOperacao)
            {
               if (filtros.EPassado)
                {
                    consulta = consulta.Where(f => f.DataDaOperacao < DateTime.Today);
                }
                else if (filtros.EFuturo)
                {
                    consulta = consulta.Where(f => f.DataDaOperacao > DateTime.Today);
                }
                else if (filtros.EHoje)
                {
                    consulta = consulta.Where(f => f.DataDaOperacao == DateTime.Today);
                }
            }

            var finacas = consulta.OrderBy(f => f.DataDaOperacao).ToList();


            return View(finacas);
        }

        public IActionResult AdicionarTransacao()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Transacoes = _context.Transacoes.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Filtrar(string[] filtro)
        {
            string id = string.Join("-", filtro);
            return RedirectToAction("Index", new {ID = id});
        }
    }
}
