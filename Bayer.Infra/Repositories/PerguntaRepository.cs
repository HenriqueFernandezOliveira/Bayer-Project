using Bayer.Domain.Entities;
using Bayer.Infra.Repositories.generic;
using System;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Bayer.Infra.Repositories
{
    public class PerguntaRepository : Repository<Pergunta>
    {
        public void AdicionarProva(string perguntaid, string provaid)
        {
            var sql = "insert into provapergunta values ('" + provaid + "','" + perguntaid + "')";

            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true";

            SqlConnection connect = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            command.Dispose();
        }

        public void AdicionarAlternativa(string alternativaid, string perguntaid)
        {
            var sql = "insert into alternativapergunta values ('" + alternativaid + "','" + perguntaid + "')";

            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;MultipleActiveResultSets=true";

            SqlConnection connect = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(sql, connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            command.Dispose();
        }
    }
}
