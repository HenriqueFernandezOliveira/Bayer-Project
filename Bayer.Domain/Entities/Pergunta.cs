using System;
using System.Collections.Generic;

namespace Bayer.Domain.Entities
{
    public class Pergunta
    {
        public Pergunta()
        {
            PerguntaId = Guid.NewGuid();
            Alternativas = new List<Alternativa>();
            Provas = new List<Prova>();
            Cadastro = new Cadastro();
        }

        public Guid PerguntaId { get; set; }
        public string Texto { get; set; }
        public Cadastro Cadastro { get; set; }
        public virtual ICollection<Alternativa> Alternativas { get; set; }
        public virtual ICollection<Prova> Provas { get; set; }
    }
}
