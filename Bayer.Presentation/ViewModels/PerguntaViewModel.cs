using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bayer.Presentation.ViewModels
{
    public class PerguntaViewModel
    {
        public PerguntaViewModel()
        {
            PerguntaId = Guid.NewGuid();
            Alternativas = new List<AlternativaViewModel>();
            Provas = new List<ProvaViewModel>();
            Cadastro = new CadastroViewModel();
        }

        [Key]
        public Guid PerguntaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DataType(DataType.MultilineText, ErrorMessage = "Tipo Inválido")]
        public string Texto { get; set; }


        public CadastroViewModel Cadastro { get; set; }
        public virtual ICollection<AlternativaViewModel> Alternativas { get; set; }
        public virtual ICollection<ProvaViewModel> Provas { get; set; }
    }
}
