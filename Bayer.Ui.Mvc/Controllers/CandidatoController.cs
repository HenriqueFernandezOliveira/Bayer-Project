using Bayer.Presentation.AppServices;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Bayer.Ui.Mvc.Controllers
{
    public class CandidatoController : Controller
    {
        private VagaAppService _vagaAppService;
        private CandidatoAppService _candidatoAppService;
        private ProvaAppService _provaAppService;
        private AlternativaAppService _alternativaAppService;
        public CandidatoController()
        {
            _candidatoAppService = new CandidatoAppService();
            _vagaAppService = new VagaAppService();
            _provaAppService = new ProvaAppService();
            _alternativaAppService = new AlternativaAppService();
        }



        [HttpGet]
        public ActionResult Vagas(string email)
        {
            if (email == "" || email == null)
            {
                email = (string)TempData.Peek("EmailCandidato");
            }

            if (email == null || email == "")
            {
                return RedirectToAction("Login", "Home", new { mensagem = "você precisa estar logado para acessar a página de inscrições" });
            }
            else
            {
                var candidato = _candidatoAppService.ObterPorEmail(email);

                ViewBag.CanddatoId = candidato.CandidatoId.ToString();

                var vagasCandidato = _candidatoAppService.ObterVagas(candidato.CandidatoId.ToString());

                return View(vagasCandidato);
            }
        }



        [HttpGet]
        public ActionResult FazerProva(string provaid)
        {
            if (!string.IsNullOrEmpty(provaid))
            {
                var prova = _provaAppService.ObterProva(provaid);

                prova.Perguntas.SelectMany(x => x.Alternativas).ToList().ForEach(x => x.Certa = false);

                return View(prova);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "Não foi possível encontrar a prova" });
            }
        }
        [HttpPost]
        public ActionResult FazerProva(ProvaViewModel prova)
        {
            var alternativas = (Dictionary<string, string>)TempData.Peek("Respostas") ?? new Dictionary<string, string>();

            var quantCertas = 0;

            var alternativasE = new List<AlternativaViewModel>();

            if (alternativas.Count > 0)
            {
                foreach (var alter in alternativas)
                {
                    var alternativa = _alternativaAppService.ObterPorId(Guid.Parse(alter.Value), true);

                    if (alternativa.Certa)
                    {
                        quantCertas++;
                    }
                    else
                    {
                        alternativasE.Add(alternativa);
                    }
                }

                SmtpClient objSmtp = new SmtpClient
                {
                    Host = "smtp.milleniumformaturas.com.br",
                    Port = 587,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("sistemas1@milleniumformaturas.com.br", "s9m8t4M$$")
                };

                var email = (string)TempData.Peek("EmailCandidato");

                MailAddress from = new MailAddress("t_hfeoliveira@tenda.com");

                List<MailAddress> to = new List<MailAddress>();
                if (!string.IsNullOrEmpty(email))
                {
                    to.Add(new MailAddress(email));
                }

                var vaga = _vagaAppService.ObterPorId(prova.ProvaId, true);

                var pfull = _provaAppService.ObterProva(prova.ProvaId.ToString());

                var subject = "Resultado da prova para " + vaga.NomeVaga + ", vaga nª " + vaga.NumVaga + "";

                var body = "<label>Olá " + email + ", aqui está o resultado da sua prova: </label> ";
                body += "<br>";
                body += "<br>";

                if (quantCertas > 6)
                {
                    body += vaga.TextoAprovacao;
                }
                else
                {
                    body += vaga.TextoReprovacao;
                }

                body += "<br/>";
                body += "<br/>";
                body += "você acertou " + quantCertas + " de " + pfull.Perguntas.Count + " questões.";
                body += "<br/>";
                body += "Estas são as perguntas que você errou: ";
                body += "<br/>";

                var pE = new List<PerguntaViewModel>();
                foreach (var p in pfull.Perguntas)
                {
                    foreach (var alt in p.Alternativas)
                    {
                        if (alternativasE.Any(x => x.AlternativaId == alt.AlternativaId))
                        {
                            pE.Add(p);
                        }
                    }
                }

                foreach (var pergunta in pE)
                {
                    body += "<br>";
                    body += pergunta.Texto;
                }

                BaseControllerMethods.SendEmail(objSmtp, from, to, body, subject, null, null);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { mensagem = "não foi possível identificar suas alternativas escolhidas, por favor tente fazer a prova novamente" });
            }

            ViewBag.Mensagem = "sua prova foi realizada com sucesso!";

            return View(prova);
        }



        public JsonResult GravaRespostasCandidato(string perguntaid, string alternativaid)
        {
            var alternativas = (Dictionary<string, string>)TempData.Peek("Respostas") ?? new Dictionary<string, string>();

            if (!alternativas.ContainsKey(perguntaid))
            {
                alternativas.Add(perguntaid, alternativaid);
            }
            else
            {
                alternativas[perguntaid] = alternativaid;
            }

            TempData["Respostas"] = alternativas;

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}
