using System;

namespace Bayer.Domain.Entities
{
    public class Cadastro
    {
        public Cadastro()
        {
            Ativo = true;
            DataCadastro = DateTime.Today;
        }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
