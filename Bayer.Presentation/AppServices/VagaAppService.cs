using AutoMapper;
using Bayer.Domain.Entities;
using Bayer.Infra.Repositories;
using Bayer.Presentation.AutoMapper;
using Bayer.Presentation.ViewModels;
using System;
using System.Collections.Generic;

namespace Bayer.Presentation.AppServices
{
    public class VagaAppService
    {
        private readonly VagaRepository _vagaRepository;
        private MapperConfiguration config;
        private readonly IMapper Mapper;

        public VagaAppService()
        {
            config = new MapperConfiguration(new AutoMapperConfig());
            Mapper = new Mapper(config);

            _vagaRepository = new VagaRepository();
        }

        public void Adicionar(VagaViewModel obj)
        {
            var vaga = Mapper.Map<VagaViewModel, Vaga>(obj);

            _vagaRepository.Adicionar(vaga);
        }

        /// <summary>
        /// Use esse método quando a vaga não está preenchida com uma prova
        /// para atualizar uma vaga preenchida utilize o método atualizarvaga
        /// </summary>
        /// <param name="obj">Não pode estar preenchida com uma prova</param>
        public void Atualizar(VagaViewModel obj)
        {
            var vaga = Mapper.Map<VagaViewModel, Vaga>(obj);

            _vagaRepository.Atualizar(vaga);
        }

        public void Excluir(Guid id)
        {
            _vagaRepository.Excluir(id);
        }

        public VagaViewModel ObterPorId(Guid id, bool readOnly)
        {
            return Mapper.Map<Vaga, VagaViewModel>(_vagaRepository.ObterPorId(id, readOnly));
        }

        public IEnumerable<VagaViewModel> ObterTodos(bool readOnly)
        {
            return Mapper.Map<IEnumerable<Vaga>, IEnumerable<VagaViewModel>>(_vagaRepository.ObterTodos(readOnly));
        }

        /// <summary>
        /// Utilize esse método para atualizar uma vaga preenchida com uma prova
        /// </summary>
        public void AtualizarVaga(VagaViewModel obj)
        {

        }

        public void Dispose()
        {
            _vagaRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
