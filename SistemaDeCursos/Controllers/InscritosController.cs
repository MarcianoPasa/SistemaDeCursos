using SistemaDeCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SistemaDeCursos.Controllers
{
    public class InscritosController : Controller
    {
        private readonly InscritosContext db = new InscritosContext();

        public ActionResult Index(int? cursoID, string cursoNome)
        {
            InscritosDB ins = new InscritosDB();
            ViewBag.CursoID = cursoID;
            ViewBag.NomeDoCurso = cursoNome;
            return View(ins.BuscarInscritosPeloCursoID(cursoID).OrderBy(x => x.pessoa_nome).ToList());
        }

        [HttpPost]
        public ActionResult InserirInscritoEmCurso(int cursoID, int pessoaID)
        {
            Inscritos inscritos = db.Inscritos.FirstOrDefault(x => x.curso_id == cursoID && x.pessoa_id == pessoaID);

            if (inscritos != null)
            {
                return Json(new { status = false, mensagem = "Pessoa já inscrita neste curso" });
            }

            InscritosDB inscritosDB = new InscritosDB();

            try
            {
                inscritosDB.InserirInscritoEmCurso(cursoID, pessoaID);
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao inserir inscrito");
            }
            finally
            {
                inscritosDB = null;
            }

            return Json(new { status = true, mensagem = string.Empty });
        }

        [HttpPost]
        public string RemoverInscritoEmCurso(int id)
        {
            InscritosDB inscritosDB = new InscritosDB();

            try
            {
                
                inscritosDB.RemoverInscritoEmCurso(id);
            }
            catch (Exception)
            {
                return "Erro ao remover inscrito";
            }
            finally
            {
                inscritosDB = null;
            }

            return string.Empty;
        }       
    }
}
