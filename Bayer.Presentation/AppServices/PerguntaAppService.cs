using AutoMapper;
using Bayer.Domain.Entities;
using Bayer.Infra.Repositories;
using Bayer.Presentation.AutoMapper;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace Bayer.Presentation.AppServices
{
    public class PerguntaAppService
    {
        private readonly PerguntaRepository _perguntaRepository;
        private MapperConfiguration config;
        private IMapper Mapper;

        public PerguntaAppService()
        {
            config = new MapperConfiguration(new AutoMapperConfig());
            Mapper = config.CreateMapper();

            _perguntaRepository = new PerguntaRepository();
        }

        public void Adicionar(PerguntaViewModel obj)
        {
            var vaga = Mapper.Map<PerguntaViewModel, Pergunta>(obj);

            _perguntaRepository.Adicionar(vaga);
        }

        public void Atualizar(PerguntaViewModel obj)
        {
            var vaga = Mapper.Map<PerguntaViewModel, Pergunta>(obj);

            _perguntaRepository.Atualizar(vaga);
        }

        public void Excluir(Guid id)
        {
            _perguntaRepository.Excluir(id);
        }

        public PerguntaViewModel ObterPorId(Guid id, bool readOnly)
        {
            return Mapper.Map<Pergunta, PerguntaViewModel>(_perguntaRepository.ObterPorId(id, readOnly));
        }

        public IEnumerable<PerguntaViewModel> ObterTodos(bool readOnly)
        {
            return Mapper.Map<IEnumerable<Pergunta>, IEnumerable<PerguntaViewModel>>(_perguntaRepository.ObterTodos(readOnly));
        }

        public void AdicionarProva(string perguntaid,string provaid)
        {
            _perguntaRepository.AdicionarProva(perguntaid, provaid);
        }

        public void AdicionarAlternativa(string alternativaid,string perguntaid)
        {
            _perguntaRepository.AdicionarAlternativa(alternativaid, perguntaid);
        }

        public void Dispose()
        {
            _perguntaRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
