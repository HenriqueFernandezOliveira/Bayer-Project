using System;
using System.Collections.Generic;

namespace Bayer.Domain.Entities
{
    public class Alternativa
    {
        public Alternativa()
        {
            AlternativaId = Guid.NewGuid();
            Perguntas = new List<Pergunta>();
            Cadastro = new Cadastro();
        }

        public Guid AlternativaId { get; set; }
        public string Texto { get; set; }
        public bool Certa { get; set; }
        public Cadastro Cadastro { get; set; }

        public virtual ICollection<Pergunta> Perguntas { get; set; }
    }
}
