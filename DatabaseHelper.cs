using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_App
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = "Server=localhost;Database=crud_app;Integrated Security=True;";

        public static List<Record> GetAllRecords()
        {
            var records = new List<Record>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM personal_details";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new Record
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Address = reader.GetString("address"),
                                State = reader.GetString("state"),
                                District = reader.GetString("district"),
                                Dob = reader.GetDateTime("dob"),
                                Language = reader.GetString("language")
                            });
                        }
                    }
                }
            }

            return records;
        }

        public static Record GetRecordById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM personal_details WHERE id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Record
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Address = reader.GetString("address"),
                                State = reader.GetString("state"),
                                District = reader.GetString("district"),
                                Dob = reader.GetDateTime("dob"),
                                Language = reader.GetString("language")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static int CreateRecord(Record record)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO personal_details 
                              (name, address, state, district, dob, language) 
                              VALUES (@name, @address, @state, @district, @dob, @language);
                              SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", record.Name);
                    command.Parameters.AddWithValue("@address", record.Address);
                    command.Parameters.AddWithValue("@state", record.State);
                    command.Parameters.AddWithValue("@district", record.District);
                    command.Parameters.AddWithValue("@dob", record.Dob);
                    command.Parameters.AddWithValue("@language", record.Language);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public static void UpdateRecord(Record record)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE personal_details SET 
                                name = @name, 
                                address = @address, 
                                state = @state, 
                                district = @district, 
                                dob = @dob, 
                                language = @language 
                                WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", record.Name);
                    command.Parameters.AddWithValue("@address", record.Address);
                    command.Parameters.AddWithValue("@state", record.State);
                    command.Parameters.AddWithValue("@district", record.District);
                    command.Parameters.AddWithValue("@dob", record.Dob);
                    command.Parameters.AddWithValue("@language", record.Language);
                    command.Parameters.AddWithValue("@id", record.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteRecord(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM personal_details WHERE id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
