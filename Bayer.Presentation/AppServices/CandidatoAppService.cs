using AutoMapper;
using Bayer.Domain.Entities;
using Bayer.Infra.Repositories;
using Bayer.Presentation.AutoMapper;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace Bayer.Presentation.AppServices
{
    public class CandidatoAppService
    {
        private readonly CandidatoRepository _candidatoRepository;
        private MapperConfiguration config;
        private IMapper Mapper;

        public CandidatoAppService()
        {
            config = new MapperConfiguration(new AutoMapperConfig());
            Mapper = config.CreateMapper();

            _candidatoRepository = new CandidatoRepository();
        }

        public void Adicionar(CandidatoViewModel obj)
        {
            var vaga = Mapper.Map<CandidatoViewModel, Candidato>(obj);

            _candidatoRepository.Adicionar(vaga);
        }

        public void Atualizar(CandidatoViewModel obj)
        {
            var vaga = Mapper.Map<CandidatoViewModel, Candidato>(obj);

            _candidatoRepository.Atualizar(vaga);
        }

        public IEnumerable<VagaViewModel> ObterVagas(string candidatoid)
        {
            return Mapper.Map<IEnumerable<Vaga>, IEnumerable<VagaViewModel>>(_candidatoRepository.ObterVagas(candidatoid));
        }

        public CandidatoViewModel ObterPorEmail(string email)
        {
            return Mapper.Map<Candidato, CandidatoViewModel>(_candidatoRepository.ObterPorEmail(email));
        }

        public void AdicionarVaga(string candidatoid,string vagaid)
        {
            _candidatoRepository.AdicionarVaga(candidatoid, vagaid);
        }

        public void Excluir(Guid id)
        {
            _candidatoRepository.Excluir(id);
        }

        public CandidatoViewModel ObterPorId(Guid id, bool readOnly)
        {
            return Mapper.Map<Candidato, CandidatoViewModel>(_candidatoRepository.ObterPorId(id, readOnly));
        }

        public IEnumerable<CandidatoViewModel> ObterTodos(bool readOnly)
        {
            return Mapper.Map<IEnumerable<Candidato>, IEnumerable<CandidatoViewModel>>(_candidatoRepository.ObterTodos(readOnly));
        }

        public void RemoverVaga(string vagaid,string candidatoid)
        {
            _candidatoRepository.RemoverVaga(vagaid,candidatoid);
        }

        public void Dispose()
        {
            _candidatoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
