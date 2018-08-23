using SistemaDeCursos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SistemaDeCursos.Controllers
{
    public class CursosController : Controller
    {
        private CursosContext db = new CursosContext();
        private readonly InscritosContext dbInscritos = new InscritosContext();

        public ActionResult Index(string ordernarPor, string ultimaOrdenacao)
        {
            if ((!string.IsNullOrEmpty(ordernarPor)) && (!string.IsNullOrEmpty(ultimaOrdenacao)))
            {
                if (ordernarPor == "NomeDoCurso")
                {
                    ordernarPor = ultimaOrdenacao == "NomeDoCurso" ? "NomeDoCursoDesc" : "NomeDoCurso";
                }
                else if (ordernarPor == "DataDeInicio")
                {
                    ordernarPor = ultimaOrdenacao == "DataDeInicio" ? "DataDeInicioDesc" : "DataDeInicio";
                }
                else if (ordernarPor == "DataDeTermino")
                {
                    ordernarPor = ultimaOrdenacao == "DataDeTermino" ? "DataDeTerminoDesc" : "DataDeTermino";
                }
            }

            List<Cursos> lista = db.Cursos.ToList();

            switch (ordernarPor)
            {
                case "NomeDoCurso":
                    lista = lista.OrderBy(x => x.curso_nome).ToList();
                    ViewBag.UltimaOrdenacao = "NomeDoCurso";
                    break;
                case "NomeDoCursoDesc":
                    lista = lista.OrderByDescending(x => x.curso_nome).ToList();
                    ViewBag.UltimaOrdenacao = "NomeDoCursoDesc";
                    break;
                case "DataDeInicioDesc":
                    lista = lista.OrderByDescending(x => x.data_inicio).ToList();
                    ViewBag.UltimaOrdenacao = "DataDeInicioDesc";
                    break;
                case "DataDeTermino":
                    lista = lista.OrderBy(x => x.data_termino).ToList();
                    ViewBag.UltimaOrdenacao = "DataDeTermino";
                    break;
                case "DataDeTerminoDesc":
                    lista = lista.OrderByDescending(x => x.data_termino).ToList();
                    ViewBag.UltimaOrdenacao = "DataDeTerminoDesc";
                    break;
                default:
                    lista = lista.OrderBy(x => x.data_inicio).ToList();
                    ViewBag.UltimaOrdenacao = "DataDeInicio";
                    break;
            }

            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "curso_id,curso_nome,data_inicio,hora_inicio,data_termino,hora_termino,descricao")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.Add(cursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cursos);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursos cursos = db.Cursos.Find(id);
            if (cursos == null)
            {
                return HttpNotFound();
            }
            return View(cursos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "curso_id,curso_nome,data_inicio,hora_inicio,data_termino,hora_termino,descricao")] Cursos cursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cursos);
        }

        [HttpPost]
        public string ExcluirCurso(string id)
        {
            InscritosDB inscritosDB = new InscritosDB();

            int cursoID = Convert.ToInt32(id);
            if (!inscritosDB.RemoverTodosOsInscritoNoCurso(cursoID))
            {
                return "Erro. Não foi possivel excluir os inscritos no curso";
            }

            try
            {
                Cursos cursos = db.Cursos.Find(Convert.ToInt32(id));
                db.Cursos.Remove(cursos);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return "Erro ao excluir o curso";
            }

            return string.Empty;
        }
    }
}
