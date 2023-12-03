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

        //public IReadOnlyList<Organiser> GetOrganisers(string filter)
        //{
        //    try
        //    {
        //        Dictionary<int, Organiser> organisers = new Dictionary<int, Organiser>();
        //        string sql = "SELECT o.id AS organiserId, o.name, o.email, o.phone, o.address, o.status, " +
        //                     "a.id AS activityId, a.fixture, a.nrOfPlaces, a.descriptionId, a.priceInfoId, a.status AS activityStatus " +
        //                     "FROM organiser o " +
        //                     "LEFT JOIN activity a ON o.id = a.organiserId " +
        //                     "WHERE o.status = 1";

        //        if (!string.IsNullOrWhiteSpace(filter))
        //        {
        //            sql += " AND (o.id LIKE @filter OR o.name LIKE @filter OR o.email LIKE @filter)";
        //        }

        //        using (SqlConnection conn = getConnection())
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            conn.Open();
        //            cmd.CommandText = sql;
        //            if (!string.IsNullOrWhiteSpace(filter))
        //            {
        //                cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
        //            }

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int organiserId = Convert.ToInt32(reader["organiserId"]);
        //                    if (!organisers.ContainsKey(organiserId))
        //                    {
        //                        Organiser organiser = new Organiser(
        //                            organiserId,
        //                            (string)reader["name"],
        //                            (string)reader["email"],
        //                            (string)reader["phone"],
        //                            (string)reader["address"],
        //                            Convert.ToInt32(reader["status"])
        //                        );
        //                        organisers.Add(organiserId, organiser);
        //                    }

        //                    if (!reader.IsDBNull(reader.GetOrdinal("activityId")))
        //                    {
        //                        Activity activity = new Activity(
        //                            Convert.ToInt32(reader["activityId"]),
        //                            (string)reader["fixture"],
        //                            Convert.ToInt32(reader["nrOfPlaces"]),
        //                            Convert.ToInt32(reader["organiserId"]),
        //                            Convert.ToInt32(reader["descriptionId"]),
        //                            Convert.ToInt32(reader["priceInfoId"]),
        //                            Convert.ToInt32(reader["activityStatus"])
        //                        );
        //                        organisers[organiserId].AddActivity(activity);
        //                    }
        //                }
        //            }
        //        }
        //        return organisers.Values.ToList();
        //    }
       
        //}

    }
}
