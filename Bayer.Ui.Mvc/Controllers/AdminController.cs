using Bayer.Presentation.AppServices;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bayer.Ui.Mvc.Controllers
{
    public class AdminController : Controller
    {
        private ProvaAppService _provaAppService;
        private VagaAppService _vagaAppService;
        private PerguntaAppService _perguntAppService;
        private AlternativaAppService _alternativaAppService;

        public AdminController()
        {
            _provaAppService = new ProvaAppService();
            _vagaAppService = new VagaAppService();
            _perguntAppService = new PerguntaAppService();
            _alternativaAppService = new AlternativaAppService();
        }


        [HttpGet]
        public ActionResult CadastrarPerguntas(string mensagem)
        {
            TempData.Remove("Alternativas");

            if ((string)TempData.Peek("EmailCandidato") == ("admin@bayer.com"))
            {
                var provas = _provaAppService.ObterTodos(true).Where(x => x.DataHoraInicio > DateTime.Today);

                ViewBag.Mensagem = mensagem;

                ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });
                return View(new PerguntaViewModel());
            }
            {
                return RedirectToAction("Error", "Home", new { mensagem = "você não tem permissão para acessar essa página" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarPerguntas(PerguntaViewModel pergunta, string ddlprova)
        {
            var alternativas = (List<AlternativaViewModel>)TempData.Peek("Alternativas") ?? new List<AlternativaViewModel>();

            if (alternativas.Count > 0)
            {
                _perguntAppService.Adicionar(pergunta);

                _perguntAppService.AdicionarProva(pergunta.PerguntaId.ToString(), ddlprova);

                foreach (var alternativa in alternativas.Distinct())
                {
                    _alternativaAppService.Adicionar(alternativa);

                    _perguntAppService.AdicionarAlternativa(alternativa.AlternativaId.ToString(), pergunta.PerguntaId.ToString());
                }

                return RedirectToAction("CadastrarPerguntas", "Admin", new { mensagem = "Pergunta cadastrada e vinculada com sucesso!" });
            }

            return RedirectToAction("CadastrarPerguntas", "Admin", new { mensagem = "Nenhuma alternativa foi cadastrada para a pergunta!" });
        }



        [HttpGet]
        public ActionResult VisualizarProva(int? mensagem)
        {
            if ((string)TempData.Peek("EmailCandidato") == ("admin@bayer.com"))
            {
                var provas = _provaAppService.ObterTodos(true).Where(x => x.DataHoraInicio > DateTime.Today);

                if (mensagem != null && mensagem.Value == 1)
                {
                    ViewBag.Mensagem = "essa prova não possui perguntas cadastradas, por favor escolha uma prova com perguntas";
                }

                ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });

                return View(new ProvaViewModel());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "você não tem permissão para acessar essa página" });
            }
        }
        [HttpPost]
        public ActionResult VisualizarProva(string ddlprova)
        {
            if (!string.IsNullOrEmpty(ddlprova))
            {
                var prova = _provaAppService.ObterProva(ddlprova);

                if (prova.Perguntas.Count > 0)
                {
                    return View(prova);
                }
                else
                {
                    return RedirectToAction("VisualizarProva", "Admin", new { mensagem = 1 });
                }

            }
            else
            {
                return View(new ProvaViewModel());
            }
        }



        [HttpGet]
        public ActionResult EditarProva(int? mensagem)
        {
            if ((string)TempData.Peek("EmailCandidato") == ("admin@bayer.com"))
            {
                var provas = _provaAppService.ObterTodos(true).Where(x => x.DataHoraInicio > DateTime.Today);

                if (mensagem != null && mensagem.Value == 1)
                {
                    ViewBag.Mensagem = "essa prova não possui perguntas cadastradas, por favor escolha uma prova com perguntas";
                }
                if (mensagem != null && mensagem.Value == 2)
                {
                    ViewBag.Mensagem = "prova atualizada com sucesso!";
                }

                ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });

                return View(new ProvaViewModel());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "você não tem permissão para acessar essa página" });
            }
        }
        [HttpPost]
        public ActionResult EditarProva(string ddlprova)
        {
            if (!string.IsNullOrEmpty(ddlprova))
            {
                var prova = _provaAppService.ObterProva(ddlprova);

                if (prova.Perguntas.Count > 0)
                {
                    return View(prova);
                }
                else
                {
                    return RedirectToAction("EditarProva", "Admin", new { mensagem = 1 });
                }
            }
            else
            {
                return View(new ProvaViewModel());
            }
        }



        [HttpGet]
        public ActionResult EditarVaga(string vagaid)
        {
            if ((string)TempData.Peek("EmailCandidato") == ("admin@bayer.com"))
            {
                try
                {
                    var vaga = _vagaAppService.ObterPorId(Guid.Parse(vagaid), true);

                    var provas = _provaAppService.ObterProvasSemVaga().Where(x => x.DataHoraInicio > DateTime.Today);
                    ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });

                    return View(vaga);
                }
                catch
                {
                    var vagas = _vagaAppService.ObterTodos(true);

                    ViewBag.Vagas = vagas.Select(x => new SelectListItem() { Text = ("Nome: " + x.NomeVaga + " / Data início inscrições: " + x.DataInicioInscricao.ToString("dd/MM/yyyy HH:mm:ss")), Value = x.VagaId.ToString() });

                    return View(new VagaViewModel());
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "você não tem permissão para acessar essa página" });
            }
        }
        [HttpPost]
        public ActionResult EditarVaga(VagaViewModel vaga, string ddlprova)
        {
            if (ModelState.IsValid)
            {
                var provas = _provaAppService.ObterProvasSemVaga().Where(x => x.DataHoraInicio > DateTime.Today);
                ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });
               
                var vagas = _vagaAppService.ObterTodos(true);
                ViewBag.Vagas = vagas.Select(x => new SelectListItem() { Text = ("Nome: " + x.NomeVaga + " / Data início inscrições: " + x.DataInicioInscricao.ToString("dd/MM/yyyy HH:mm:ss")), Value = x.VagaId.ToString() });
               
                try
                {
                    var id = Guid.Parse(ddlprova);

                    vaga.Prova = _provaAppService.ObterPorId(id, true);

                    _vagaAppService.AtualizarVaga(vaga);
                }
                catch
                {
                    _vagaAppService.Atualizar(vaga);
                }               

                ViewBag.Mensagem = "A vaga foi atualizada com sucesso!";

                vaga.Prova = null;

                return View(vaga);
            }
            else
            {
                ViewBag.Mensagem = "alguns erros foram detectados ao atualizar a vaga";

                var provas = _provaAppService.ObterProvasSemVaga().Where(x => x.DataHoraInicio > DateTime.Today);
                ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });

                return View(vaga);
            }
        }


                
        public JsonResult GravarAlternativa(string texto, bool certa)
        {
            var alternativas = (List<AlternativaViewModel>)TempData.Peek("Alternativas") ?? new List<AlternativaViewModel>();
            var alternativa = new AlternativaViewModel()
            {
                Texto = texto,
                Certa = certa
            };
            alternativas.Add(alternativa);

            var newAlternativa = new AlternativaViewModel()
            {
                Texto = null,
                Certa = false
            };

            var html = BaseControllerMethods.RenderPartialViewToString(this, "_alternativaCreate", newAlternativa);

            TempData["Alternativas"] = alternativas;

            return Json(new { html = html }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult AtualizarPergunta(string textoPergunta, string perguntaid)
        {
            var listaPerguntasEdit = (Dictionary<string, string>)TempData["PerguntasEdit"] ?? new Dictionary<string, string>();

            if (!listaPerguntasEdit.ContainsKey(perguntaid))
            {
                listaPerguntasEdit.Add(perguntaid, textoPergunta);
            }
            else
            {
                listaPerguntasEdit[perguntaid] = textoPergunta;
            }

            TempData["PerguntasEdit"] = listaPerguntasEdit;

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AtualizarAlternativa(string textoAlternativa, string alternativaid, bool certa)
        {
            var listaAlternativas = (List<AlternativaViewModel>)TempData["AlternativasEdit"] ?? new List<AlternativaViewModel>();

            if (listaAlternativas.FirstOrDefault(x => x.AlternativaId.Equals(Guid.Parse(alternativaid))) == null)
            {
                var alternativa = _alternativaAppService.ObterPorId(Guid.Parse(alternativaid), true);

                alternativa.Texto = textoAlternativa;
                alternativa.Certa = certa;

                listaAlternativas.Add(alternativa);
            }
            else
            {
                listaAlternativas.ForEach(x =>
                {
                    if (x.AlternativaId.Equals(Guid.Parse(alternativaid)))
                    {
                        x.Texto = textoAlternativa;
                        x.Certa = certa;
                        return;
                    };
                });
            }

            TempData["AlternativasEdit"] = listaAlternativas;

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AtualizarProva(string provaid, string datainicio, string tempoprova)
        {
            var prova = _provaAppService.ObterPorId(Guid.Parse(provaid), true);
            prova.DataHoraInicio = DateTime.Parse(datainicio);
            prova.TempoProva = TimeSpan.Parse(tempoprova);
            prova.DataHoraTermino = prova.DataHoraInicio.Add(prova.TempoProva);

            _provaAppService.Atualizar(prova);

            var listaAlternativas = (List<AlternativaViewModel>)TempData["AlternativasEdit"] ?? new List<AlternativaViewModel>();

            var listaPerguntasEdit = (Dictionary<string, string>)TempData["PerguntasEdit"] ?? new Dictionary<string, string>();

            foreach (var pergunta in listaPerguntasEdit)
            {
                var perguntaEdit = _perguntAppService.ObterPorId(Guid.Parse(pergunta.Key), false);
                perguntaEdit.Texto = pergunta.Value;
                _perguntAppService.Atualizar(perguntaEdit);
            }

            foreach (var alternativa in listaAlternativas)
            {
                _alternativaAppService.Atualizar(alternativa);
            }

            return Json(new { sucess = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult CriarVaga(string ddlprova)
        {
            if ((string)TempData.Peek("EmailCandidato") == ("admin@bayer.com"))
            {
                string provaid = ddlprova ?? "";

                if (provaid == "")
                {
                    var provas = _provaAppService.ObterProvasSemVaga().Where(x => x.DataHoraInicio > DateTime.Today);

                    ViewBag.Provas = provas.Select(x => new SelectListItem() { Text = ("Data Inicio: " + x.DataHoraInicio.ToString("dd/MM/yyy HH:mm:ss") + " / Tempo Prova: " + x.TempoProva.Hours.ToString() + " / Data Término: " + x.DataHoraTermino.ToString("dd/MM/yyy HH:mm:ss")), Value = x.ProvaId.ToString() });

                    return View(new VagaViewModel());
                }
                else
                {
                    var prova = _provaAppService.ObterPorId(Guid.Parse(provaid.ToString()), true);
                    var vaga = new VagaViewModel() { Prova = prova };
                    return View(vaga);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "você não tem permissão para acessar essa página" });
            }
        }
        [HttpPost]
        public ActionResult CriarVaga(VagaViewModel vaga)
        {
            var provaid = Request.Form["provaid"];

            if (ModelState.IsValid)
            {
                var prova = _provaAppService.ObterPorId(Guid.Parse(provaid), true);

                vaga.Prova = prova;
                //vaga.VagaId = prova.ProvaId;
                _vagaAppService.Adicionar(vaga);

                return RedirectToAction("ListaVagas", "Home");
            }

            return View(vaga);
        }



        [HttpGet]
        public ActionResult CriarProva(string mensagem)
        {

            if ((string)TempData.Peek("EmailCandidato") == ("admin@bayer.com"))
            {
                ViewBag.mensagem = mensagem;
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "você não tem permissão para acessar essa página" });
            }

            return View(new ProvaViewModel());
        }
        [HttpPost]
        public ActionResult CriarProva(ProvaViewModel prova)
        {
            prova.DataHoraTermino = prova.DataHoraInicio.AddHours(prova.TempoProva.Hours);
            if (ModelState.IsValid)
            {
                _provaAppService.Adicionar(prova);

                return RedirectToAction("CriarProva", "Admin", new { mensagem = "A prova foi criada com sucesso" });
            }
            else
            {
                return View(prova);
            }
        }

    }
}
