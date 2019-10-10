using System;
using System.Collections.Generic;

namespace Bayer.Domain.Entities
{
    public class Vaga
    {
        public Vaga()
        {
            VagaId = Guid.NewGuid();
            Candidatos = new List<Candidato>();
            Cadastro = new Cadastro();
        }

        public Guid VagaId { get; set; }

        public int NumVaga { get; set; }

        public string NomeVaga { get; set; }
        public string ListaRequisitos { get; set; }
        public string TextoAprovacao { get; set; }
        public string TextoReprovacao { get; set; }
        public DateTime DataInicioInscricao { get; set; }
        public DateTime DataTerminoInscricao { get; set; }
        public Cadastro Cadastro { get; set; }

        public virtual Prova Prova { get; set; }
        public virtual ICollection<Candidato> Candidatos { get; set; }
    }
}
