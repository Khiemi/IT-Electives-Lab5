using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace StudentSafeApp
{
    public class StudentRepository
    {
        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public void AddStudent(string studentId, string name, string course)
        {
            const string sql = @"
                INSERT INTO Students (StudentId, Name, Course)
                VALUES (@StudentId, @Name, @Course);";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 20).Value = studentId;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name;
                command.Parameters.Add("@Course", SqlDbType.NVarChar, 100).Value = course;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetAllStudents()
        {
            const string sql = @"
                SELECT StudentId, Name, Course
                FROM Students
                ORDER BY StudentId;";

            var table = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(table);
            }

            return table;
        }

        public int UpdateStudent(string studentId, string name, string course)
        {
            const string sql = @"
                UPDATE Students
                SET Name = @Name,
                    Course = @Course
                WHERE StudentId = @StudentId;";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 20).Value = studentId;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name;
                command.Parameters.Add("@Course", SqlDbType.NVarChar, 100).Value = course;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public int DeleteStudent(string studentId)
        {
            const string sql = @"
                DELETE FROM Students
                WHERE StudentId = @StudentId;";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 20).Value = studentId;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}