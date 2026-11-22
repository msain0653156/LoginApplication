using System;
using System.Data;
using System.Data.SqlClient;

namespace LoginApplication
{
    public class UserDatabase
    {
        private string connectionString = @"Server=localhost\SQLEXPRESS01;Database=LoginDB;Integrated Security=True;TrustServerCertificate=True;";

        public bool CreateUser(string username, string password)
        {
            byte[] salt = PasswordHelper.CreateSalt();
            byte[] hash = PasswordHelper.HashPassword(password, salt);

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO Users (Username, PasswordHash, PasswordSalt) VALUES (@Username, @Hash, @Salt)", connection))
            {
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
                command.Parameters.Add("@Hash", SqlDbType.VarBinary, 32).Value = hash;
                command.Parameters.Add("@Salt", SqlDbType.VarBinary, 16).Value = salt;

                connection.Open();

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2601 || ex.Number == 2627)
                        return false;

                    throw;
                }
            }
        }

        public bool Login(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT PasswordHash, PasswordSalt FROM Users WHERE Username = @Username", connection))
            {
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;

                    byte[] hash = (byte[])reader["PasswordHash"];
                    byte[] salt = (byte[])reader["PasswordSalt"];

                    return PasswordHelper.VerifyPassword(password, salt, hash);
                }
            }
        }
    }
}
