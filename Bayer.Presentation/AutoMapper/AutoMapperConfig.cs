using AutoMapper;
using AutoMapper.Configuration;
using Bayer.Domain.Entities;
using Bayer.Presentation.ViewModels;

namespace Bayer.Presentation.AutoMapper
{
    public class AutoMapperConfig : MapperConfigurationExpression
    {
        public AutoMapperConfig()
        {

            CreateMap<CadastroViewModel, Cadastro>();
            CreateMap<Cadastro, CadastroViewModel>();

            CreateMap<VagaViewModel, Vaga>();
            CreateMap<Vaga, VagaViewModel>();

            CreateMap<ProvaViewModel, Prova>();
            CreateMap<Prova, ProvaViewModel>();

            CreateMap<PerguntaViewModel, Pergunta>();
            CreateMap<Pergunta, PerguntaViewModel>();


            CreateMap<CandidatoViewModel, Candidato>();
            CreateMap<Candidato, CandidatoViewModel>();


            CreateMap<AlternativaViewModel, Alternativa>();
            CreateMap<Alternativa, AlternativaViewModel>();
        }
        //public static void RegisterMappings()
        //{
        //    Mapper.Initialize(x =>
        //    {
        //        x.AddProfile<DomainToViewModelMappingProfile>();
        //        x.AddProfile<ViewModelToDomainMappingProfile>();
        //    });
        //}
    }
}
