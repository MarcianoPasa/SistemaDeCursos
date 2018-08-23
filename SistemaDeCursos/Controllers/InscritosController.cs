using SistemaDeCursos.Models;
using System;
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
        public string InserirInscritoEmCurso(int cursoID, int pessoaID)
        {
            Inscritos inscritos = db.Inscritos.FirstOrDefault(x => x.curso_id == cursoID && x.pessoa_id == pessoaID);
            if (inscritos != null)
            {
                return "Pessoa já inscrita neste curso";
            }

            try
            {
                InscritosDB inscritosDB = new InscritosDB();
                inscritosDB.InserirInscritoEmCurso(cursoID, pessoaID);
            }
            catch (Exception)
            {
                return "Erro ao inserir inscrito";
            }

            return string.Empty;
        }

        [HttpPost]
        public string RemoverInscritoEmCurso(int id)
        {
            try
            {
                InscritosDB inscritosDB = new InscritosDB();
                inscritosDB.RemoverInscritoEmCurso(id);
            }
            catch (Exception)
            {
                return "Erro ao remover inscrito";
            }

            return string.Empty;
        }
    }
}
