using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace SistemaDeCursos.Models
{
    public class InscritosDB : DbContext
    {
        public List<Inscritos> BuscarInscritosPeloCursoID(int? cursoID)
        {
            List<Inscritos> listaDeInscritos = new List<Inscritos>();

            try
            {
                string sql = string.Format(@"
                    SELECT 
                        dbo.inscritos.inscricao_id, 
                        dbo.inscritos.pessoa_id, 
                        dbo.pessoas.pessoa_nome 
                    FROM 
                        dbo.cursos 
                    LEFT JOIN 
                        dbo.inscritos ON dbo.cursos.curso_id = dbo.inscritos.curso_id 
                    LEFT JOIN 
                        dbo.pessoas ON dbo.inscritos.pessoa_id = dbo.pessoas.pessoa_id 
                    {0}",
                    cursoID != null ? " WHERE dbo.cursos.curso_id = @cursoID " : string.Empty
                );

                NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PgCursos"].ToString());
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);

                if (cursoID != null)
                {
                    command.Parameters.Add(new NpgsqlParameter("@cursoID", cursoID));
                }

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Inscritos inscritos = new Inscritos
                    {
                        inscricao_id = reader.GetInt32(0),
                        pessoa_id = reader.GetInt32(1),
                        pessoa_nome = reader.GetString(2)
                    };

                    listaDeInscritos.Add(inscritos);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return listaDeInscritos;
        }

        public bool InserirInscritoEmCurso(int cursoID, int pessoaID)
        {
            try
            {
                string sql = string.Format(@"
                    INSERT INTO dbo.inscritos (
	                    inscricao_id, curso_id, pessoa_id
                    )
	                VALUES (
                        default, @cursoID, @pessoaID
                    )"
                );

                NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PgCursos"].ToString());
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);

                command.Parameters.Add(new NpgsqlParameter("@cursoID", cursoID));
                command.Parameters.Add(new NpgsqlParameter("@pessoaID", pessoaID));

                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoverInscritoEmCurso(int inscricaoID)
        {
            try
            {
                string sql = string.Format(@"
                    DELETE FROM dbo.inscritos WHERE dbo.inscritos.inscricao_id = @inscricaoID
                ");

                NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PgCursos"].ToString());
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);

                command.Parameters.Add(new NpgsqlParameter("@inscricaoID", inscricaoID));

                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoverTodosOsInscritoNoCurso(int cursoID)
        {
            try
            {
                string sql = string.Format(@"
                    DELETE FROM dbo.inscritos WHERE dbo.inscritos.curso_id = @cursoID
                ");

                NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PgCursos"].ToString());
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);

                command.Parameters.Add(new NpgsqlParameter("@cursoID", cursoID));

                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}