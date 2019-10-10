using Bayer.Presentation.AppServices;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bayer.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private VagaAppService _vagaAppService;
        private CandidatoAppService _candidatoAppService;
        private ProvaAppService _provaAppService;
        public HomeController()
        {
            _vagaAppService = new VagaAppService();
            _candidatoAppService = new CandidatoAppService();
            _provaAppService = new ProvaAppService();
        }

        public ActionResult Index()        
        {
            var email = (string)TempData.Peek("EmailCandidato");

            if (email != null && !email.Equals("admin@bayer.com"))
            {
                var User = _candidatoAppService.ObterPorEmail(email);

                if (User != null)
                {
                    var listavagas = _candidatoAppService.ObterVagas(User.CandidatoId.ToString());
                    ViewBag.ListaVagas = listavagas;
                }
                else
                {
                    ViewBag.ListaVagas = new List<VagaViewModel>();
                }
            }
            else
            {
                ViewBag.ListaVagas = new List<VagaViewModel>();
            }

            return View(_vagaAppService.ObterTodos(true).Where(x => x.DataTerminoInscricao >= DateTime.Today).ToList());
        }



        public ActionResult NossaHistoria()
        {
            return View();
        }



        public ActionResult Desinscricao(string vagaid, string candidatoid)
        {
            var email = (string)TempData.Peek("EmailCandidato");

            _candidatoAppService.RemoverVaga(vagaid, candidatoid);

            return RedirectToAction("Vagas", "Candidato", new { email });
        }



        public ActionResult ProgramaEstagio()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        [HttpGet]
        public ActionResult RecuperarSenha()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecuperarSenha(string email)
        {
            var candidato = _candidatoAppService.ObterPorEmail(email);
            if (candidato != null)
            {
                SmtpClient objSmtp = new SmtpClient
                {
                    Host = "smtp.milleniumformaturas.com.br",
                    Port = 587,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("sistemas1@milleniumformaturas.com.br", "s9m8t4M$$")
                };

                MailMessage objEmail = new MailMessage();

                objEmail.From = new MailAddress("t_hfeoliveira@tenda.com");

                objEmail.To.Add(email);

                var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("MudarSenha", "Home"),
                    Query = ("email=" + email),
                };

                Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString();
                objEmail.Body += "<br>";
                objEmail.Body += "<br>";
                objEmail.Body = "<label>Olá " + email + ".</label> ";
                objEmail.Body = "<br>";
                objEmail.Body = "<label>Entre nesse link para redefinir sua senha </label >"
                    + "<br>"
                    + "<br>"
                    + "<a href='" + url + "'>link</a>";

                objEmail.IsBodyHtml = true;

                objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                objSmtp.Send(objEmail);

                objSmtp.Dispose();
                return RedirectToAction("Login", "Home", new { mensagem = "verifique o seu email e redefina sua senha" });
            }
            else
            {
                return RedirectToAction("Registrar", "Home", new { mensagem = "não foi encontrado nenhum cadastro com o email fornecido" });
            }
        }



        public ActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }



        public ActionResult ListaVagas()
        {
            var email = (string)TempData.Peek("EmailCandidato");

            if (email != null && !email.Equals("admin@bayer.com"))
            {
                var User = _candidatoAppService.ObterPorEmail(email);

                if (User != null)
                {
                    var listavagas = _candidatoAppService.ObterVagas(User.CandidatoId.ToString());
                    ViewBag.ListaVagas = listavagas;
                }
                else
                {
                    ViewBag.ListaVagas = new List<VagaViewModel>();
                }
            }
            else
            {
                ViewBag.ListaVagas = new List<VagaViewModel>();
            }

           
            return View(_vagaAppService.ObterTodos(true).Where(x => x.DataTerminoInscricao >= DateTime.Today).ToList());
        }



        [HttpGet]
        public ActionResult Premios()
        {

            return View();
        }



        [HttpGet]
        public ActionResult Inscricao(Guid vagaid)
        {
            var email = (string)TempData.Peek("EmailCandidato");

            if (email != null && !email.Equals("admin@bayer.com"))
            {
                var vaga = _vagaAppService.ObterPorId(vagaid, true);

                return View(vaga);
            }

            return RedirectToAction("login", "Home", new {mensagem = "você precisa estar logado para se inscrever para a vaga" });
        }
        [HttpPost]
        public ActionResult Inscricao(VagaViewModel v)
        {
            var user = _candidatoAppService.ObterPorEmail(Request.Form["emailUser"]);

            var vagasUser = _candidatoAppService.ObterVagas(user.CandidatoId.ToString());

            if (!vagasUser.Any(x => x.VagaId.Equals(v.VagaId)))
            {
                _candidatoAppService.AdicionarVaga(user.CandidatoId.ToString(), v.VagaId.ToString());               
            }

            return RedirectToAction("Vagas", "Candidato");
        }



        public ActionResult DetalhesVaga(Guid vagaid)
        {
            var vaga = _vagaAppService.ObterPorId(vagaid, true);

            return View(vaga);
        }



        [HttpGet]
        public ActionResult Login(string mensagem)
        {
            ViewBag.Mensagem = mensagem;

            return View();
        }
        [HttpPost]
        public ActionResult Login(CandidatoViewModel candidato)
        {
            var user = _candidatoAppService.ObterPorEmail(candidato.Username);

            if (user == null)
            {
                if (candidato.Username.Equals("admin@bayer.com") && candidato.Senha.Equals("admin"))
                {
                    TempData["EmailCandidato"] = candidato.Username;
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Login", "Home", new { mensagem = "não foi possível fazer o login, verifique se digitou email e senha corretamente" });
            }
            else 
            if(user.Senha.Equals(candidato.Senha))
            {
                TempData["EmailCandidato"] = user.Username;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home", new { mensagem = "não foi possível fazer o login, verifique se digitou email e senha corretamente" });               
            }        
                       
        }

        public ActionResult Error(string mensagem)
        {
            ViewBag.Mensagem = mensagem;

            return View();
        }

        [HttpGet]
        public ActionResult Registrar(string mensagem)
        {
            ViewBag.Mensagem = mensagem;

            return View();
        }

        [HttpPost]
        public ActionResult Registrar(CandidatoViewModel candidato)
        {
            var user = _candidatoAppService.ObterPorEmail(candidato.Username);

            if (user != null)
            {
                return RedirectToAction("Registrar", "Home", new { mensagem = "Já existe uma conta com esse email!" });
            }
            if (ModelState.IsValid)
            {
                _candidatoAppService.Adicionar(candidato);
                return RedirectToAction("Login", "Home", new { mensagem = "sua conta foi criado com sucesso" });
            }
            else
            {
                return RedirectToAction("Registrar", "Home", new { mensagem = "Algo deu errado ao realizar o seu cadastro" });
            }
        }

        [HttpGet]
        public ActionResult MudarSenha(string email)
        {
            var candidato = _candidatoAppService.ObterPorEmail(email);

            ViewBag.Email = email;

            return View(candidato);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MudarSenha(CandidatoViewModel candidato)
        {
            var user = _candidatoAppService.ObterPorEmail(candidato.Username);
            user.Senha = candidato.Senha;

            try
            {
                _candidatoAppService.Atualizar(user);

                return RedirectToAction("Login", "Home", new { mensagem = "sua senha foi alterada com sucesso!" });
            }
            catch
            {
                return RedirectToAction("Error", "Home", new { mensagem = "erro ao atuaizar cadastro" });
            }

        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}