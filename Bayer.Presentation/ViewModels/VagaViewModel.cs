using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bayer.Presentation.ViewModels
{
    public class VagaViewModel
    {
        public VagaViewModel()
        {
            VagaId = Guid.NewGuid();
            Candidatos = new List<CandidatoViewModel>();
            Cadastro = new CadastroViewModel();
        }

        [Key]
        public Guid VagaId { get; set; }

        public string ListaRequisitos { get; set; }
        public long NumVaga { get; set; }

        public string NomeVaga { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DataType(DataType.MultilineText, ErrorMessage = "Tipo Inválido")]
        public string TextoAprovacao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DataType(DataType.MultilineText, ErrorMessage = "Tipo Inválido")]        
        public string TextoReprovacao { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DisplayFormat(ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true, DataFormatString = "yyyy-MM-dd")]
        [Display(Name = "Data de inicio das inscrições")]
        public DateTime DataInicioInscricao { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [DisplayFormat(ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true, DataFormatString = "yyyy-MM-dd")]
        [Display(Name = "Data de término das inscrições")]
        public DateTime DataTerminoInscricao { get; set; }


        public CadastroViewModel Cadastro { get; set; }
        public virtual ProvaViewModel Prova { get; set; }
        public virtual ICollection<CandidatoViewModel> Candidatos { get; set; }
    }
}
