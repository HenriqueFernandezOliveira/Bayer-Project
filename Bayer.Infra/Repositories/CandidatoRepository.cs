using Bayer.Domain.Entities;
using Bayer.Infra.Repositories.generic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Bayer.Infra.Repositories
{
    public class CandidatoRepository : Repository<Candidato>
    {
        public IEnumerable<Vaga> ObterVagas(string candidatoid)
        {
            List<Vaga> vagas = new List<Vaga>();

            if (!string.IsNullOrEmpty(candidatoid))
            {
                var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;";

                var sql = "select v.* from vaga v inner join candidatoVaga cv on cv.vaga_vagaid = v.vagaId inner join Candidato c on c.candidatoId = cv.candidato_candidatoid where v.cadastro_ativo = 1" +
                    " and c.CandidatoId = '"+candidatoid+"'";                              

                SqlConnection connect = new SqlConnection(conn);
               
                SqlCommand command = new SqlCommand(sql, connect);

                connect.Open();
                SqlDataReader nwReader = command.ExecuteReader();
                while (nwReader.Read())
                {
                    if (nwReader.HasRows)
                    {
                        var vaga = new Vaga()
                        {
                            VagaId = Guid.Parse(nwReader["VagaId"].ToString()),
                            NumVaga = int.Parse(nwReader["NumVaga"].ToString()),
                            NomeVaga = nwReader["NomeVaga"].ToString(),
                            DataInicioInscricao = DateTime.Parse(nwReader["DataInicioInscricao"].ToString()),
                            DataTerminoInscricao = DateTime.Parse(nwReader["DataTerminoInscricao"].ToString())
                        };

                        vaga.Prova = Db.Provas.AsNoTracking().First(x => x.ProvaId == vaga.VagaId);

                        vagas.Add(vaga);
                    }
                }               
                connect.Close();
                command.Dispose();
            }

            return vagas;
        }


        public void AdicionarVaga(string candidatoid, string vagaid)
        {
            var sql = "insert into candidatovaga values ('" + candidatoid + "','" + vagaid + "')";

            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true";

            SqlConnection connect = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            command.Dispose();
        }

        public void RemoverVaga(string vagaid, string candidatoid)
        {
            var sql = "delete from candidatovaga where candidato_candidatoid = '"+ candidatoid +"' and vaga_vagaid = '"+ vagaid +"' ";

            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true";

            SqlConnection connect = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            command.Dispose();
        }

        public Candidato ObterPorEmail(string email)
        {
            return DbSet.AsNoTracking().Where(x => x.Username.Equals(email,StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
