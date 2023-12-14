using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
    public class etlapService
    {
		MySqlConnection connection;
		public etlapService()
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Server = "localhost";
			builder.Port = 3306;
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "etlapdb";

			connection = new MySqlConnection(builder.ConnectionString);
		}
		public List<etel> GetAll()
		{
			List<etel> etelek = new List<etel>();
			OpenConnection();
			string sql = "SELECT * FROM etlap";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					etel etel = new etel();
					etel.id = reader.GetInt32("id");
					etel.nev = reader.GetString("nev");
					etel.leiras = reader.GetString("leiras");
					etel.ar = reader.GetInt32("ar");
					etel.kategoria = reader.GetString("kategoria");
					etelek.Add(etel);
				}
			}
			CloseConnection();
			return etelek;
		}
		public bool UpdateEgyElemSzazalek(int id, etel etel1, int emeles)
		{
			OpenConnection();
			string sql = @"UPDATE etlap SET ar = @price WHERE id = @id";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@price", etel1.ar * (1+ (emeles/100.0)));
			command.Parameters.AddWithValue("@id", id);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}
		public bool UpdateEgyElemFt(int id, etel etel1, int emeles)
		{
			OpenConnection();
			string sql = @"UPDATE etlap SET ar = @price WHERE id = @id";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@price", etel1.ar + emeles);
			command.Parameters.AddWithValue("@id", id);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}
		public bool UpdateTobbElemFt(int emeles)
		{
			OpenConnection();
			string sql = @"UPDATE etlap SET ar = ar + @price";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@price", emeles);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}
		public bool UpdateTobbElemSzazalek(int emeles)
		{
			OpenConnection();
			string sql = @"UPDATE etlap SET ar = ar * (1 + @price / 100)";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@price", emeles);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}
		public bool Create(etel etel1)
		{
			OpenConnection();
			string sql = "INSERT INTO etlap(nev,leiras,ar,kategoria) VALUES (@name,@desc,@price,@cat)";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@name", etel1.nev);
			command.Parameters.AddWithValue("@desc", etel1.leiras);
			command.Parameters.AddWithValue("@price", etel1.ar);
			command.Parameters.AddWithValue("@cat", etel1.kategoria);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}
		public bool Delete(int id)
		{
			OpenConnection();
			string sql = "DELETE FROM etlap WHERE id = @id";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@id", id);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}
		private void CloseConnection()
		{
			if (connection.State == System.Data.ConnectionState.Open)
			{
				connection.Close();
			}
		}
		private void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open)
			{
				connection.Open();
			}
		}
	}
}
