using AutoMapper;
using Bayer.Domain.Entities;
using Bayer.Infra.Repositories;
using Bayer.Presentation.AutoMapper;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace Bayer.Presentation.AppServices
{
    public class AlternativaAppService
    {
        private readonly AlternativaRepository _alternativaRepository;
        private MapperConfiguration config;
        private IMapper Mapper;

        public AlternativaAppService()
        {
            config = new MapperConfiguration(new AutoMapperConfig());
            Mapper = config.CreateMapper();

            _alternativaRepository = new AlternativaRepository();
        }

        public void Adicionar(AlternativaViewModel obj)
        {
            var vaga = Mapper.Map<AlternativaViewModel, Alternativa>(obj);

            _alternativaRepository.Adicionar(vaga);
        }

        public void Atualizar(AlternativaViewModel obj)
        {
            var vaga = Mapper.Map<AlternativaViewModel, Alternativa>(obj);

            _alternativaRepository.Atualizar(vaga);
        }

        public void Excluir(Guid id)
        {
            _alternativaRepository.Excluir(id);
        }

        public AlternativaViewModel ObterPorId(Guid id, bool readOnly)
        {
            return Mapper.Map<Alternativa, AlternativaViewModel>(_alternativaRepository.ObterPorId(id, readOnly));
        }

        public IEnumerable<AlternativaViewModel> ObterTodos(bool readOnly)
        {
            return Mapper.Map<IEnumerable<Alternativa>, IEnumerable<AlternativaViewModel>>(_alternativaRepository.ObterTodos(readOnly));
        }

        public void Dispose()
        {
            _alternativaRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
