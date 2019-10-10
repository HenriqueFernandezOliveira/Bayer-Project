using Bayer.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bayer.Infra.EntityConfig
{
    public class CandidatoConfig : EntityTypeConfiguration<Candidato>
    {
        public CandidatoConfig()
        {
            Property(x => x.Username).IsRequired();
            Property(x => x.Senha).IsRequired();
            HasMany(x => x.Vagas).WithMany(v => v.Candidatos);
        }
    }
}
