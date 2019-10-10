using Bayer.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bayer.Infra.EntityConfig
{
    public class AlternativaConfig : EntityTypeConfiguration<Alternativa>
    {
        public AlternativaConfig()
        {
            HasMany(x => x.Perguntas).WithMany(p => p.Alternativas);
            Property(x => x.Texto).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
        }
    }
}
