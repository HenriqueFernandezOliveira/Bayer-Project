using AutoMapper;
using Bayer.Domain.Entities;
using Bayer.Infra.Repositories;
using Bayer.Presentation.AutoMapper;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace Bayer.Presentation.AppServices
{
    public class ProvaAppService
    {
        private readonly ProvaRepository _provaRepository;
        private MapperConfiguration config;
        private IMapper Mapper;

        public ProvaAppService()
        {
            config = new MapperConfiguration(new AutoMapperConfig());
            Mapper = config.CreateMapper();

            _provaRepository = new ProvaRepository();
        }

        public void Adicionar(ProvaViewModel obj)
        {
            var prova = Mapper.Map<ProvaViewModel, Prova>(obj);

            _provaRepository.Adicionar(prova);
        }

        public void Atualizar(ProvaViewModel obj)
        {
            var prova = Mapper.Map<ProvaViewModel, Prova>(obj);

            _provaRepository.Atualizar(prova);
        }

        public ProvaViewModel ObterProva(string provaid)
        {
            return Mapper.Map<Prova, ProvaViewModel>(_provaRepository.ObterProva(provaid));
        }

        public void Excluir(Guid id)
        {
            _provaRepository.Excluir(id);
        }

        public IEnumerable<ProvaViewModel> ObterProvasSemVaga()
        {
            return Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(_provaRepository.ObterProvasSemVaga());
        }

        public IEnumerable<ProvaViewModel> ObterProvasSemPerguntas()
        {
            return Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(_provaRepository.ObterProvasSemPerguntas());
        }

        public ProvaViewModel ObterPorId(Guid id, bool readOnly)
        {
            return Mapper.Map<Prova, ProvaViewModel>(_provaRepository.ObterPorId(id, readOnly));
        }

        public IEnumerable<ProvaViewModel> ObterTodos(bool readOnly)
        {
            return Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(_provaRepository.ObterTodos(readOnly));
        }

        public void Dispose()
        {
            _provaRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
