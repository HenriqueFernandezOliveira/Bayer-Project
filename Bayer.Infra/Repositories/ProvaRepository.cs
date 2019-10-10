using Bayer.Domain.Entities;
using Bayer.Infra.Repositories.generic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Data;

namespace Bayer.Infra.Repositories
{
    public class ProvaRepository : Repository<Prova>
    {
        public IEnumerable<Prova> ObterProvasSemVaga()
        {
            return DbSet.Where(x => x.Vaga == null);
        }

        public IEnumerable<Prova> ObterProvasSemPerguntas()
        {
            List<Prova> provas = new List<Prova>();

            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;";

            var sql = "select * from prova where provaid not in(select prova_provaid from provapergunta) and cadastro_ativo = 1";

            SqlConnection connect = new SqlConnection(conn);

            SqlCommand command = new SqlCommand(sql, connect);

            connect.Open();
            SqlDataReader nwReader = command.ExecuteReader();
            while (nwReader.Read())
            {
                if (nwReader.HasRows)
                {
                    var prova = new Prova()
                    {
                        ProvaId = Guid.Parse(nwReader["ProvaId"].ToString()),
                        DataHoraInicio = DateTime.Parse(nwReader["DataHoraInicio"].ToString()),
                        TempoProva = TimeSpan.Parse(nwReader["TempoProva"].ToString()),
                        DataHoraTermino = DateTime.Parse(nwReader["DataHoraTermino"].ToString())                       
                    };

                    provas.Add(prova);
                }
            }

            connect.Close();
            command.Dispose();

            return provas;
        }

        public Prova ObterProva(string provaid)
        {
            var guid = Guid.Parse(provaid);

            var prova = DbSet.Include(x => x.Perguntas).First(x => x.ProvaId.Equals(guid));

            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;";
            SqlConnection connectPergunta = new SqlConnection(conn);
            connectPergunta.Open();

            #region Get Perguntas         

            var sqlPerguntas = "select per.* from prova p inner join provapergunta pp on pp.Prova_ProvaId = p.ProvaId inner join Pergunta per on per.PerguntaId = pp.Pergunta_PerguntaId" +
                " where per.cadastro_ativo = 1 and p.Provaid = '" + provaid + "'";
            List<Pergunta> perguntas = new List<Pergunta>();

            SqlCommand commandPergunta = new SqlCommand(sqlPerguntas, connectPergunta);

            SqlDataReader nwReader = commandPergunta.ExecuteReader();
            while (nwReader.Read())
            {
                if (nwReader.HasRows)
                {
                    var pergunta = new Pergunta()
                    {
                        PerguntaId = Guid.Parse(nwReader["PerguntaId"].ToString()),
                        Texto = nwReader["Texto"].ToString()
                    };

                    perguntas.Add(pergunta);
                }
            }
            nwReader.Close();
            //connectPergunta.Close();
            commandPergunta.Dispose();

            #endregion

            prova.Perguntas.ToList().AddRange(perguntas);

            #region Get Alternativas
            foreach (var pergunta in prova.Perguntas)
            {            
                var sqlAlternativa = "select a.* from alternativa a inner join alternativapergunta ap on ap.alternativa_alternativaid = a.alternativaid inner join pergunta p on p.perguntaid = ap.Pergunta_PerguntaId " +
                    " where a.cadastro_ativo = 1 and p.perguntaid = '" + pergunta.PerguntaId.ToString() + "' ";

                SqlCommand commandAlternativa = new SqlCommand(sqlAlternativa, connectPergunta);
                SqlDataReader nwReader2 = commandAlternativa.ExecuteReader();
                while (nwReader2.Read())
                {
                    if (nwReader2.HasRows)
                    {
                        var alternativa = new Alternativa()
                        {
                            AlternativaId = Guid.Parse(nwReader2["AlternativaId"].ToString()),
                            Texto = nwReader2["Texto"].ToString(),
                            Certa = bool.Parse(nwReader2["Certa"].ToString())
                        };
                        pergunta.Alternativas.Add(alternativa);
                    }
                }
                nwReader2.Close();
            }

            connectPergunta.Close();

            #endregion

            return prova;
        }

        public override void Atualizar(Prova obj)
        {
            var conn = "Data Source=SQL5046.site4now.net;Initial Catalog=DB_A4E62F_Bayer;User Id=DB_A4E62F_Bayer_admin;Password=caneta333;";

            obj.DataHoraTermino = obj.DataHoraInicio.Add(obj.TempoProva);

            var sql = "update prova set DataHoraInicio = @datahorainicio, TempoProva = @tempoprova, DataHoraTermino = @datatermino where provaid = '"+obj.ProvaId+"'";

            SqlConnection connect = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(sql, connect);

            command.Parameters.Add("@datahorainicio", SqlDbType.DateTime2).Value = obj.DataHoraInicio;
            command.Parameters.Add("@datatermino", SqlDbType.DateTime2).Value = obj.DataHoraTermino;
            command.Parameters.Add("@tempoprova", SqlDbType.Time).Value = obj.TempoProva;           

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
            command.Dispose();
        }
    }
}
