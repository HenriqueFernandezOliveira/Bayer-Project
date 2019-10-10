using Bayer.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bayer.Infra.EntityConfig
{
    public class PerguntaConfig : EntityTypeConfiguration<Pergunta>
    {
        public PerguntaConfig()
        {
            HasMany(x => x.Alternativas).WithMany(c => c.Perguntas);
            Property(x => x.Texto).IsRequired().HasColumnType("nvarchar").HasMaxLength(300);
        }
    }
}
