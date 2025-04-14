using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace POC_Leitura_Imagem
{
    public class RegistroOCRRepository
    {
        private string connectionString;

        public RegistroOCRRepository()
        {
            string dsn = "X";
            string uid = "X";
            string pwd = "X";
            connectionString = $"Dsn={dsn};Uid={uid};Pwd={pwd};";
        }

        public bool TestarConexao(out string mensagem)
        {
            try
            {
                using (var conn = new OdbcConnection(connectionString))
                {
                    conn.Open();
                    mensagem = "Conexão com o banco de dados realizada com sucesso.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                mensagem = $"Erro ao conectar ao banco: {ex.Message}";
                return false;
            }
        }

        public List<int> ObterAutosPorLote(int numeroLote)
        {
            var lista = new List<int>();

            using (var conn = new OdbcConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT nr_aiip FROM infracao WHERE nr_lote = ?";
                using (var cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nr_lote", numeroLote);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.TryParse(reader["nr_aiip"].ToString(), out int auto))
                            {
                                lista.Add(auto);
                            }
                        }
                    }
                }
            }

            return lista;
        }
    }
}