using SistemaDeCursos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SistemaDeCursos.Controllers
{
    public class PessoasController : Controller
    {
        private PessoasContext db = new PessoasContext();
        private readonly InscritosContext dbInscritos = new InscritosContext();

        public ActionResult Index(string ordernarPor, string ultimaOrdenacao)
        {
            if ((!string.IsNullOrEmpty(ordernarPor)) && (!string.IsNullOrEmpty(ultimaOrdenacao)))
            {
                if (ordernarPor == "NomeDaPessoa")
                {
                    ordernarPor = ultimaOrdenacao == "NomeDaPessoa" ? "NomeDaPessoaDesc" : "NomeDaPessoa";
                }
                else if (ordernarPor == "DataDeNascimento")
                {
                    ordernarPor = ultimaOrdenacao == "DataDeNascimento" ? "DataDeNascimentoDesc" : "DataDeNascimento";
                }
            }

            List<Pessoas> lista = db.Pessoas.ToList();

            switch (ordernarPor)
            {
                case "NomeDaPessoaDesc":
                    lista = lista.OrderByDescending(x => x.pessoa_nome).ToList();
                    ViewBag.UltimaOrdenacao = "NomeDaPessoaDesc";
                    break;
                case "DataDeNascimento":
                    lista = lista.OrderBy(x => x.data_nascimento).ToList();
                    ViewBag.UltimaOrdenacao = "DataDeNascimento";
                    break;
                case "DataDeNascimentoDesc":
                    lista = lista.OrderByDescending(x => x.data_nascimento).ToList();
                    ViewBag.UltimaOrdenacao = "DataDeNascimentoDesc";
                    break;
                default:
                    lista = lista.OrderBy(x => x.pessoa_nome).ToList();
                    ViewBag.UltimaOrdenacao = "NomeDaPessoa";
                    break;
            }

            return View(lista);
        }

        public ActionResult Create()
        {
            ViewBag.Sexo = new[]
            {
                new SelectListItem(){ Value = "-1", Text = "", Selected = true },
                new SelectListItem(){ Value = "0", Text = "Masculino"},
                new SelectListItem(){ Value = "1", Text = "Feminino"}
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pessoa_id,pessoa_nome,data_nascimento,sexo,telefone_fixo,telefone_movel,observacoes")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                pessoas.telefone_fixo = FormatarTelefone(pessoas.telefone_fixo);
                pessoas.telefone_movel = FormatarTelefone(pessoas.telefone_movel);

                db.Pessoas.Add(pessoas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pessoas);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pessoas pessoas = db.Pessoas.Find(id);
            if (pessoas == null)
            {
                return HttpNotFound();
            }

            ViewBag.Sexo = new[]
            {
                new SelectListItem(){ Value = "0", Text = "Masculino", Selected = pessoas.sexo == 0},
                new SelectListItem(){ Value = "1", Text = "Feminino", Selected = pessoas.sexo == 1}
            };

            return View(pessoas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pessoa_id,pessoa_nome,data_nascimento,sexo,telefone_fixo,telefone_movel,observacoes")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                pessoas.telefone_fixo = FormatarTelefone(pessoas.telefone_fixo);
                pessoas.telefone_movel = FormatarTelefone(pessoas.telefone_movel);

                db.Entry(pessoas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pessoas);
        }

        [HttpPost]
        public string ExcluirPessoa(string id)
        {
            long pessoaID = Convert.ToInt64(id);

            Inscritos inscritos = dbInscritos.Inscritos.FirstOrDefault(x => x.pessoa_id == pessoaID);
            if (inscritos != null)
            {
                return "Esta pessoa não pode ser excluida porque esta inscriata em um ou mais cursos";
            }

            try
            {
                Pessoas pessoas = db.Pessoas.Find(Convert.ToInt32(id));
                db.Pessoas.Remove(pessoas);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return "Erro ao excluir a pessoa";
            }

            return string.Empty;
        }

        [HttpPost]
        public JsonResult BuscaPessoaPorAutocompletar(string buscarPor)
        {
            return Json(
                db.Pessoas.Where(x => x.pessoa_nome.ToLower().Contains(buscarPor.ToLower())).Select(y => new { label = y.pessoa_nome, id = y.pessoa_id }).ToList(),
                JsonRequestBehavior.AllowGet
            );
        }

        private string FormatarTelefone(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return null;
            }

            string novoValor = valor.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace("_", string.Empty).Replace(" ", string.Empty);

            if (novoValor.Length == 10)
            {
                return string.Format("{0:(##) ####-####}", Convert.ToInt64(novoValor));
            }
            else if (novoValor.Length == 11)
            {
                return string.Format("{0:(##) #####-####}", Convert.ToInt64(novoValor));
            }
            else
            {
                return valor.Replace("_", "");
            }
        }
    }
}