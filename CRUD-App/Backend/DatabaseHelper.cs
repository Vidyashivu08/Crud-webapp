using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

using Backend.Models;

namespace Backend
{
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static void SetConnectionString(string connectionStringName)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public static List<Record> GetAllRecords()
        {
            var records = new List<Record>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM personal_details";
                using (var command = new MySqlCommand(query, connection))
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
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM personal_details WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
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
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO personal_details 
                              (name, address, state, district, dob, language) 
                              VALUES (@name, @address, @state, @district, @dob, @language);
                              SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
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
            using (var connection = new MySqlConnection(connectionString))
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

                using (var command = new MySqlCommand(query, connection))
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
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM personal_details WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
