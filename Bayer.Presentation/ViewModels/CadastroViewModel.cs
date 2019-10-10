using System;

namespace Bayer.Presentation.ViewModels
{
    public class CadastroViewModel
    {
        public CadastroViewModel()
        {
            Ativo = true;
            DataCadastro = DateTime.Today;
        }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
