using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bayer.Presentation.ViewModels
{
    public class CandidatoViewModel
    {
        public CandidatoViewModel()
        {
            CandidatoId = Guid.NewGuid();
            Vagas = new List<VagaViewModel>();
            Cadastro = new CadastroViewModel();
        }

        [Key]
        public Guid CandidatoId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Senha muito curta")]
        public string Senha { get; set; }


        public CadastroViewModel Cadastro { get; set; }
        public virtual ICollection<VagaViewModel> Vagas { get; set; }

        public virtual ICollection<ProvaViewModel> Provas { get; set; }
    }
}
