using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bayer.Presentation.ViewModels
{
    public class ProvaViewModel
    {
        public ProvaViewModel()
        {
            ProvaId = Guid.NewGuid();
            Cadastro = new CadastroViewModel();
            //DataHoraTermino = DataHoraInicio.AddHours(TempoProva.Hours);
            Perguntas = new List<PerguntaViewModel>();
        }

        [Key]
        public Guid ProvaId { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [Display(Name = "Data e Hora Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true, DataFormatString = "yyyy-MM-dd")]        
        public DateTime DataHoraInicio { get; set; }

        [DataType(DataType.Time)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Não pode ser nulo")]
        [Display(Name = "Tempo de prova")]
        public TimeSpan TempoProva { get; set; }

        [Display(Name = "Data  e Hora Término")]
        [ScaffoldColumn(false)]
        public DateTime DataHoraTermino { get; set; }
        

        public CadastroViewModel Cadastro { get; set; }
        public virtual ICollection<PerguntaViewModel> Perguntas { get; set; }
        public virtual VagaViewModel Vaga { get; set; }
    }
}
