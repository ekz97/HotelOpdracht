using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class OrganisorRepository
    {
        private string connectionString;

        public OrganisorRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public IReadOnlyList<Organiser> GetOrganisers()
        {
            try
            {
                Dictionary<int, Organiser> organisers = new Dictionary<int, Organiser>();
                string sql = "SELECT  o.id AS organizerId, o.name, a.id AS activityId, a.fixture,a.nrOfPlaces,d.id AS descriptionId,d.duration,d.location,d.explanation, d.name AS descriptionName, p.id AS priceInfoId, p.adultPrice, p.childPrice, p.discount FROM dbo.Organiser o JOIN dbo.Activity a ON o.id = a.organiserId JOIN dbo.Description d ON a.descriptionId = d.id JOIN dbo.PriceInfo p ON a.priceInfoId = p.id WHERE o.status = 1 AND a.status = 1 AND d.status = 1 AND p.status = 1;";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["organiserId"]);
                            if (!organisers.ContainsKey(id))
                            {
                                Organiser organiser = new Organiser(id, (string)reader["name"]);
                                organisers.Add(id, organiser);
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("activityId")))
                            {

                                Activity activity = new Activity(Convert.ToInt32(reader["activityId"]), (DateTime)reader["fixture"], Convert.ToInt32(reader["nrOfPlaces"]), new Description(Convert.ToInt32(reader["duration"]), (string)reader["location"], (string)reader["explanation"], (string)reader["descriptionName"]), new PriceInfo(Convert.ToInt32(reader["adultPrice"]), (Convert.ToInt32(reader["childPrice"]), (Convert.ToInt32(reader["discount"])
                            }
                        }
                    }
                }
            
            }

            catch
            {

            }


        }

    }
}
