using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public IReadOnlyList<Customerr> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int,Customerr> customers = new Dictionary<int, Customerr>();
                string sql = "select t1.id,t1.name customername,t1.email,t1.phone,t1.address,t2.name membername,t2.birthday\r\nfrom customer t1 left join (select * from member where status=1) t2 on t1.id=t2.customerId\r\nwhere t1.status=1";
                if (!string.IsNullOrWhiteSpace(filter)) 
                {
                    sql += " and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }
                using(SqlConnection conn = getConnection()) 
                using(SqlCommand cmd = conn.CreateCommand()) 
                { 
                    conn.Open();
                    cmd.CommandText = sql;
                    if (!string.IsNullOrWhiteSpace(filter)) cmd.Parameters.AddWithValue("@filter",$"%{filter}%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id))
                            {
                                Customerr customer = new Customerr(id, (string)reader["customername"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"])));
                                customers.Add(id, customer);
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                Member member = new Member((string)reader["membername"], (DateTime)reader["birthday"]);
                                customers[id].AddMember(member);
                            }
                        }      
                    }
                }
                return customers.Values.ToList();
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("getcustomer", ex);
            }
        }
        public Customerr GetCustomerById(int? customerId)
        {
            Customerr customer = null;

            try
            {
                string sql = @"SELECT t1.id, t1.name AS customername, t1.email, t1.phone, t1.address, t2.name AS membername, t2.birthday
                       FROM customer t1
                       LEFT JOIN member t2 ON t1.id = t2.customerId
                       WHERE t1.status = 1 AND t1.id = @customerId";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (customer == null)
                            {
                                customer = new Customerr(
                                    Convert.ToInt32(reader["ID"]),
                                    (string)reader["customername"],
                                    new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"]))
                                );
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                Member member = new Member((string)reader["membername"], (DateTime)reader["birthday"]);
                                customer.AddMember(member);
                            }
                        }
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("getcustomerbyid", ex);
            }
        }
        public void AddCustomer(Customerr customer)
        {
            try
            {
                string insertCustomerQuery = "INSERT INTO Customer(name, email, phone, address, status) VALUES(@name, @email, @phone, @address, @status)";
                string insertMemberQuery = "INSERT INTO Member(customerId, name, birthday, status) VALUES (@customerid, @name, @birthday, @status)";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = insertCustomerQuery;

                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.Contact.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.Contact.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.Contact.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);

                        cmd.ExecuteNonQuery();

                        // Assuming the database assigns the ID automatically, retrieve the new ID
                        cmd.CommandText = "SELECT @@IDENTITY";
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());

                        foreach (Member member in customer.GetMembers())
                        {
                            cmd.CommandText = insertMemberQuery;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@customerid", newId);
                            cmd.Parameters.AddWithValue("@name", member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.Birthday);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("addcustomer", ex);
            }
        }
        public void UpdateCustomer(Customerr customer)
        {
            try
            {
                string updateCustomerSql = "UPDATE Customer SET name = @name, email = @email, phone = @phone, address = @address WHERE ID = @customerId";
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = updateCustomerSql;
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.Contact.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.Contact.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.Contact.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@customerId", customer.Id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Handle case where customer with given ID does not exist
                            throw new CustomerRepositoryException("Customer not found for update");
                        }

                        // Nu moeten de leden van de klant worden bijgewerkt
                        string deleteMembersSql = "DELETE FROM Member WHERE customerId = @customerId";
                        cmd.CommandText = deleteMembersSql;
                        cmd.ExecuteNonQuery();

                        // Voeg de bijgewerkte leden toe
                        foreach (Member member in customer.GetMembers())
                        {
                            string insertMemberSql = "INSERT INTO Member(customerId, name, birthday, status) VALUES (@customerId, @name, @birthday, @status)";
                            cmd.CommandText = insertMemberSql;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@customerId", customer.Id);
                            cmd.Parameters.AddWithValue("@name", member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.Birthday);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }

                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw new CustomerRepositoryException("Failed to update customer", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("Failed to update customer", ex);
            }
        }
        public void DeleteCustomer(int customerId)
        {
            try
            {
                string updateCustomerStatusSql = "UPDATE Customer SET status = 0 WHERE ID = @customerId";
                string updateMemberStatusSql = "UPDATE Member SET status = 0 WHERE customerId = @customerId";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();

                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = updateCustomerStatusSql;
                        cmd.Parameters.AddWithValue("@customerId", customerId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Handle case where customer with given ID does not exist
                            throw new CustomerRepositoryException("Customer not found for deletion");
                        }

                        // Update status of associated members with the customer
                        cmd.CommandText = updateMemberStatusSql;
                        cmd.Parameters.Clear(); // Clear previous parameters
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.ExecuteNonQuery();

                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw new CustomerRepositoryException("Failed to delete customer", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("Failed to delete customer", ex);
            }
        }
    }
}
