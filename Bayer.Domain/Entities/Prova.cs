using System;
using System.Collections.Generic;

namespace Bayer.Domain.Entities
{
    public class Prova
    {
        public Prova()
        {
            ProvaId = Guid.NewGuid();
            Cadastro = new Cadastro(); ;
            DataHoraTermino = DataHoraInicio.Add(TempoProva);
        }

        public Guid ProvaId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public TimeSpan TempoProva { get; set; }
        public DateTime DataHoraTermino { get; set; }
        public Cadastro Cadastro { get; set; }

        public virtual ICollection<Pergunta> Perguntas { get; set; }
        public virtual ICollection<Candidato> Candidatos { get; set; }
        public virtual Vaga Vaga { get; set; }
    }
}
