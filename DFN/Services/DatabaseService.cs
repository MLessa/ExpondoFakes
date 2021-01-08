using MySql.Data.MySqlClient;

namespace DFN.Services
{
	public class DatabaseService
	{
		private readonly string cs = "SERVER=50.62.209.51;Initial Catalog='dflix';Persist Security Info=no;User Name='dflix';Password='sdflix#123'";

		public dynamic GetDenounceData()
		{
			dynamic result;
			using (var con = new MySqlConnection(cs))
			{
				con.Open();
				var cmd = new MySqlCommand("select * from expondoFakes", con);
				using (MySqlDataReader rdr = cmd.ExecuteReader())
				{
					rdr.Read();
					result = new { Count = (int)rdr.GetInt32(0), LastDenounceDate = rdr.GetDateTime(1) };
					rdr.Close();
				}
				con.Close();
			}
			return result;
		}

		public bool IncrementDenounce()
		{
			using (var con = new MySqlConnection(cs))
			{
				con.Open();
				var cmd = new MySqlCommand("update expondoFakes set CountDenounces = CountDenounces + 1", con);
				cmd.ExecuteNonQuery();
				con.Close();
				return true;
			}
		}

	}
}