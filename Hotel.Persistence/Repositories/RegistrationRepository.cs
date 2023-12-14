using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {

        private string connectionString;

        public RegistrationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
       
        public void AddRegistration(Registrationn registration, int customerId)
        {
            try
            {
                string insertRegistration = "INSERT INTO Registration (activityId) VALUES (@activityId); SELECT SCOPE_IDENTITY();";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = new SqlCommand(insertRegistration, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@activityId", registration.Activity.Id);

                    int newRegistrationId = Convert.ToInt32(cmd.ExecuteScalar());

                    string insertMemberRegistration = "INSERT INTO MemberRegistration (memberName, memberBirthday, memberCustomerId, registrationId) VALUES (@memberName, @memberBirthday, @memberCustomerId, @registrationId)";

          
          
                    foreach (Member member in registration.Members)
                    {
                        cmd.CommandText = insertMemberRegistration;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@memberCustomerId", customerId);
                        cmd.Parameters.AddWithValue("@registrationId", newRegistrationId);
                        cmd.Parameters.AddWithValue("@memberName", member.Name);
                        cmd.Parameters.AddWithValue("@memberBirthday", member.Birthday);

                        cmd.ExecuteNonQuery();
             
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException("Failed to add registration", ex);
            }

        }


        public IReadOnlyList<Member> GetRegistratedMembersForActivity(int customerId , int activityId)
        {

            try
            {
                List<Member> members = new List<Member>();

                string sql = "SELECT * FROM Registration r JOIN MemberRegistration mr ON r.id = mr.RegistrationId WHERE r.ActivityId = @activityId AND memberCustomerId = @customerId;";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@activityId", activityId);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Member member = new Member((string)reader["memberName"], (DateTime)reader["memberBirthday"]);
                            members.Add(member);

                        }
                    }

                    return members;

                }
            }

            catch(Exception ex)
            {
                throw new RegistrationRepositoryException("GetRegistratedMembersForActivity", ex);
            }
            

        }

    }
}

