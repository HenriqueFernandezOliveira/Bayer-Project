using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bayer.Presentation.ViewModels
{
    public class AlternativaViewModel
    {
        public AlternativaViewModel()
        {
            AlternativaId = Guid.NewGuid();
            Perguntas = new List<PerguntaViewModel>();
            Cadastro = new CadastroViewModel();
        }

        [Key]
        public Guid AlternativaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DataType(DataType.Text, ErrorMessage = "Tipo Inválido")]
        public string Texto { get; set; }
        public bool Certa { get; set; }


        public CadastroViewModel Cadastro { get; set; }
        public virtual ICollection<PerguntaViewModel> Perguntas { get; set; }
    }
}
