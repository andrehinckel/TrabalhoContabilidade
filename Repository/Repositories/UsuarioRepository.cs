using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool Delete(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "DELETE FROM usuarios WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(Usuario usuario)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"INSERT INTO usuarios (login, senha, data_nascimento, id_contabilidade)
OUTPUT INSERTED.ID VALUES(@LOGIN, @SENHA, @DATA_NASCIMENTO, @ID_CONTABILIDADE)";
            command.Parameters.AddWithValue("@LOGIN", usuario.Login);
            command.Parameters.AddWithValue("@SENHA", usuario.Senha);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            command.Parameters.AddWithValue("@ID_CONTABILIDADE", usuario.IdContabilidade);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public Usuario ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT * FROM WHERE id @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }
            DataRow row = table.Rows[0];
            Usuario usuario = new Usuario();
            usuario.Login = row["login"].ToString();
            usuario.Senha = row["senha"].ToString();
            usuario.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);
            usuario.IdContabilidade = Convert.ToInt32(row["id_contabilidade"]);

            return usuario;
        }

        public List<Usuario> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT contabilidades.id AS 'ContabilidadeId', contabilidades.nome AS 'ContabilidadeNome',
usuarios.id AS 'Id',
usuarios.login AS 'Login',
usuarios.senha AS 'Senha',
usuarios.data_nascimento AS 'DataNascimento' FROM usuarios INNER JOIN contabilidades ON(usuarios.id_contabilidade = contabilidades.id)";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<Usuario> usuarios = new List<Usuario>();
            command.Connection.Close();
            foreach(DataRow row in table.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Id = Convert.ToInt32(row["Id"]);
                usuario.Login = row["Login"].ToString();
                usuario.Senha = row["Senha"].ToString();
                usuario.DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                usuario.IdContabilidade = Convert.ToInt32(row["ContabilidadeId"]);

                usuario.Contabilidade = new Contabilidade();
                usuario.Contabilidade.Id = Convert.ToInt32(row["ContabilidadeId"]);
                usuario.Contabilidade.Nome = row["ContabilidadeNome"].ToString();

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public bool Update(Usuario usuario)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"UPDATE usuarios SET login = @LOGIN, senha = @SENHA, data_nascimento = @DATA_NASCIMENTO, id_contabilidade = @ID_CONTABILIDADE WHERE id = @ID";
            command.Parameters.AddWithValue("@LOGIN", usuario.Login);
            command.Parameters.AddWithValue("@SENHA", usuario.Senha);
            command.Parameters.AddWithValue("@DATA_NASCIMENTO", usuario.DataNascimento);
            command.Parameters.AddWithValue("@ID_CONTABILIDADE", usuario.IdContabilidade);
            command.Parameters.AddWithValue("@ID", usuario.Id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
