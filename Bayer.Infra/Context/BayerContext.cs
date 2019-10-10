using Bayer.Domain.Entities;
using Bayer.Infra.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Bayer.Infra.Context
{
    public class BayerContext : DbContext
    {
        public BayerContext() : base("Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }
                
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //dizendo ao entity framework que a propriedade que estiver com o nome da classe + Id é a chave primária
            modelBuilder.Properties().Where(c => c.Name == c.ReflectedType.Name + "Id").Configure(p => p.IsKey());

            //dizendo ao entity framework que a propriedades do tipo string devem ser criadas como varchar no banco de dados
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(50));

            modelBuilder.Configurations.Add(new VagaConfig());
            modelBuilder.Configurations.Add(new ProvaConfig());
            modelBuilder.Configurations.Add(new PerguntaConfig());
            modelBuilder.Configurations.Add(new CandidatoConfig());
            modelBuilder.Configurations.Add(new AlternativaConfig());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Prova> Provas { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
    }
}
