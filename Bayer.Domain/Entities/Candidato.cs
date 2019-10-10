using System;
using System.Collections.Generic;

namespace Bayer.Domain.Entities
{
    public class Candidato
    {
        public Candidato()
        {
            CandidatoId = Guid.NewGuid();
            Vagas = new List<Vaga>();
            Cadastro = new Cadastro();
        }

        public Guid CandidatoId { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public Cadastro Cadastro { get; set; }

        public virtual ICollection<Prova> Provas { get; set; }
        public virtual ICollection<Vaga> Vagas { get; set; }
    }
}
