using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Npgsql;

namespace PostToEventHub
{
    public class EventHubPoster
    {
        private static EventHubClient _ehClient = null;
        public static void PostEventData(string jsonString)
        {
            if (!string.IsNullOrEmpty(Program.Options.EventHubConnectionString))
            {
                PostHubData(jsonString);
            }

            if (!string.IsNullOrEmpty(Program.Options.SqlServerConnectionString))
            {
                PostSqlServerData(jsonString);
            }

            if (!string.IsNullOrEmpty(Program.Options.PostgreSqlServerConnectionString))
            {
                PostPostgreSqlData(jsonString);
            }
        }

        private static void PostPostgreSqlData(string jsonString)
        {
            try
            {
                using (var conn = new NpgsqlConnection(Program.Options.PostgreSqlServerConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;

                        // Insert some data
                        cmd.CommandText = Program.Options.PostgreSqlPostQuery.Replace("{DATA}",
                            jsonString.Replace("'", "''"));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception posting data to PostgreSQL server: " + ex.Message);
            }
        }

        private static void PostSqlServerData(string jsonString)
        {
            
            SqlConnection conn = new SqlConnection(Program.Options.SqlServerConnectionString);
            try
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                // yeah I know just don't judge me on this one
                cmd.CommandText = Program.Options.SqlPostQuery.Replace("{DATA}", jsonString.Replace("'", "''"));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception posting data to SQL server: " + ex.Message);

                try
                {
                    conn.Close();
                }
                catch
                {
                }
            }
        }

        private static void PostHubData(string jsonString)
        {
            try
            {
                if (_ehClient == null)
                {
                    _ehClient = EventHubClient.CreateFromConnectionString(Program.Options.EventHubConnectionString);
                    Console.WriteLine("Created Event Hub Client, Path = " + _ehClient.Path);
                }

                _ehClient.Send(new EventData(Encoding.UTF8.GetBytes(jsonString)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception sending event: " + ex.Message);
            }
        }
    }
}
