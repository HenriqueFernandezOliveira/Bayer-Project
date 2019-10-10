using Bayer.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bayer.Infra.EntityConfig
{
    public class ProvaConfig : EntityTypeConfiguration<Prova>
    {
        public ProvaConfig()
        {
            HasMany(x => x.Perguntas).WithMany(p => p.Provas);
            HasOptional(x => x.Vaga).WithRequired(x => x.Prova);
            Property(x => x.DataHoraInicio).IsRequired();
            Property(x => x.DataHoraTermino).IsRequired();
            Property(x => x.TempoProva).IsRequired();
        }

    }
}
