using Bayer.Domain.Entities;
using Bayer.Infra.Repositories.generic;
using System;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;

namespace Bayer.Infra.Repositories
{
    public class VagaRepository : Repository<Vaga>
    {
        public override void Adicionar(Vaga obj)
        {
            if(obj.Prova != null)
            {
                var sql = "insert into vaga(vagaid,nomevaga,listarequisitos,textoaprovacao,textoreprovacao,datainicioinscricao,dataterminoinscricao,cadastro_ativo,cadastro_datacadastro)" +
                    "values('"+obj.Prova.ProvaId+"','"+obj.NomeVaga+"','"+obj.ListaRequisitos+"','"+obj.TextoAprovacao+"','"+obj.TextoReprovacao+"'," +
                    "cast('"+obj.DataInicioInscricao.ToString("yyyy-MM-dd HH:mm:ss")+"' as datetime2),cast('"+obj.DataTerminoInscricao.ToString("yyyy-MM-dd HH:mm:ss")+"' as datetime2),1,cast('"+DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss")+"' as datetime2))";

                var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true";

                SqlConnection connect = new SqlConnection(conn);
                SqlCommand command = new SqlCommand(sql, connect);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                command.Dispose();
            }           
        }

        public override Vaga ObterPorId(Guid id, bool readOnly)
        {
            if (readOnly)
            {
                return DbSet.Include(x => x.Prova).AsNoTracking().FirstOrDefault(x => x.VagaId == id);
            }
            else
            {
                return DbSet.Include(x => x.Prova).FirstOrDefault(x => x.VagaId == id);
            }
          
        }

        public void AtualizarVaga(Vaga obj)
        {
            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true";

            var sql = "update vaga set nomevaga = '"+obj.NomeVaga+"',listarequisitos = '"+obj.ListaRequisitos+"', vagaid = '"+obj.Prova.ProvaId+"' where vagaid = '"+obj.VagaId+"'";

            SqlConnection connect = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            command.Dispose();
        }
    }
}
