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

        public IActionResult AdicionarCategoria()
        {
            var categoria = new Categoria { CategoriaId = "categoria" };

            return View(categoria);
        }

        public IActionResult BalancoGeral()
        {
            var registrosFinanceiros = from f in _context.Financas
                                                        .Include(c => c.Categoria)
                                                        .Include(t => t.Transacao)
                                                        .ToList()
                                       group f by new { f.CategoriaId } into total
                                       select new 
                                       {
                                           CategoriaNome = total.First().Categoria.Nome,
                                           TransacaoNome = total.First().Transacao.Nome,
                                           DataOperacao = total.First().DataDaOperacao,
                                           ValorTotal = total.Sum(v => v.Valor)
                                       };

            var lucros = _context.Financas
                                .Include (c => c.Categoria)
                                .Include(t => t.Transacao)
                                .Where(x => x.Transacao.Nome == "lucro")
                                .Sum(v => v.Valor);

            var gastos = _context.Financas
                                .Include(c => c.Categoria)
                                .Include(t => t.Transacao)
                                .Where(x => x.Transacao.Nome == "despesa")
                                .Sum(v => v.Valor);

            var diferenca = lucros - gastos;

            List<RegistrosFinanceiros> registros = new List<RegistrosFinanceiros>();

            foreach(var item in registrosFinanceiros)
            {
                var registro = new RegistrosFinanceiros
                {
                    CategoriaNome = item.CategoriaNome,
                    TransacaoNome = item.TransacaoNome,
                    DataOperacao = item.DataOperacao.Value.ToString("dd/MM/yyyy"),
                    ValorCategoria = item.ValorTotal.Value.ToString("F"),
                    Lucros = lucros.Value.ToString("F"),
                    Despesas = gastos.Value.ToString("F"),
                    Diferenca = diferenca.Value.ToString("F"),
                };
                registros.Add(registro);
            }

            return View(registros);

        }

        [HttpPost]
        public IActionResult Filtrar(string[] filtro)
        {
            string id = string.Join("-", filtro);
            return RedirectToAction("Index", new {ID = id});
        }

        [HttpPost]
        public IActionResult AdicionarCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {

                var categoriaBanco = new Categoria
                {
                    CategoriaId = categoria.Nome.ToLower(),
                    Nome = categoria.Nome,
                };

                _context.Categorias.Add(categoriaBanco);
                _context.SaveChanges();
                return RedirectToAction("Index");

            } else
            {
                return View(categoria);
            }
        }

        [HttpPost]
        public IActionResult AdicionarTransacao(Financeiro financeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Financas.Add(financeiro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            { 
                ViewBag.Categorias = _context.Categorias.ToList();
                ViewBag.Transacoes = _context.Transacoes.ToList();
                return View(financeiro);
            }
        }

        public IActionResult RemoverTransacao(int id)
        {
            var transacao = _context.Financas.Find(id);
            if (transacao != null)
            {
                _context.Financas.Remove(transacao);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
