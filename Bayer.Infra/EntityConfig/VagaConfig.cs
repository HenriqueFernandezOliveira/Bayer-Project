using Bayer.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Bayer.Infra.EntityConfig
{
    public class VagaConfig : EntityTypeConfiguration<Vaga>
    {
        public VagaConfig()
        {
            Property(x => x.NumVaga).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true })).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TextoAprovacao).IsRequired().HasColumnType("nvarchar").HasMaxLength(300);
            Property(x => x.TextoReprovacao).IsRequired().HasColumnType("nvarchar").HasMaxLength(300);
            Property(x => x.DataInicioInscricao).IsRequired();
            Property(x => x.DataTerminoInscricao).IsRequired();
            Property(x => x.ListaRequisitos).IsOptional();
        }
    }
}
