//using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrudAppUsingAdo.Models
{
    public class EmployeeDbContext
    {
        private readonly string ? _connectionString;
        public EmployeeDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetAllEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee()
                {
                    id = Convert.ToInt32(dr["id"]),
                    name = dr["name"].ToString(),
                    address = dr["address"].ToString(),
                    salary = Convert.ToDouble(dr["salary"]),
                    joining_date = DateTime.Now,
                    gender = dr["gender"].ToString(),
                    age = Convert.ToInt32(dr["age"])
                };
                employees.Add(employee);


            }
            return employees;
        }
        public bool AddEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_AddEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", employee.name);
            cmd.Parameters.AddWithValue("@address", employee.address);
            cmd.Parameters.AddWithValue("@salary", employee.salary);
            cmd.Parameters.AddWithValue("@joining_date", employee.joining_date);
            cmd.Parameters.AddWithValue("gender", employee.gender);
            cmd.Parameters.AddWithValue("@age", employee.age);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpdateEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", employee.id);
            cmd.Parameters.AddWithValue("@name", employee.name);
            cmd.Parameters.AddWithValue("@address", employee.address);
            cmd.Parameters.AddWithValue("@salary", employee.salary);
            cmd.Parameters.AddWithValue("@joining_date", employee.joining_date);
            cmd.Parameters.AddWithValue("gender", employee.gender);
            cmd.Parameters.AddWithValue("@age", employee.age);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }



        }
        public bool DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
