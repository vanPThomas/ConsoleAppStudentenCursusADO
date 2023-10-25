using System.Data.SqlClient;

namespace ConsoleAppStudentenCursusADO
{
    public  class Databeheer
    {
        private string connectionString;

        public Databeheer(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public void VoegCursusToe(Cursus cursus)
        {
            string sql = "INSERT INTO dbo.cursus(naam, klasid) VALUEs (@naam, @klasid)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@klasid", System.Data.SqlDbType.Int));
                    command.Parameters["@naam"].Value = cursus.Naam;
                    command.Parameters["@klasid"].Value = cursus.KlasId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void VoegKlasToe(Klas klas)
        {
            string sql = "INSERT INTO dbo.klas(klasnaam, lokaal) VALUES (@klasnaam, @lokaal)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@klasnaam", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@lokaal", System.Data.SqlDbType.NVarChar));
                    command.Parameters["@klasnaam"].Value = klas.klasnaam;
                    command.Parameters["@lokaal"].Value = klas.lokaal;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public Cursus GeefCursus(int id)
        {
            string sql = "SELECT * FROM cursus WHERE id=@id";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Cursus cursus = new Cursus((string)reader["naam"], (int)reader["id"]);
                    reader.Close();
                    return cursus;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
        }

        public List<Cursus> GeefCursussen()
        {
            List<Cursus> cursussen = new List<Cursus>();
            string sql = "SELECT * FROM cursus";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        cursussen.Add(new Cursus((int)reader["id"], (string)reader["naam"], (int)reader["klasid"]));
                    }
                    reader.Close();
                    return cursussen;
                }
                catch(Exception ex)
                {
                    Console.WriteLine (ex.ToString());
                    throw;
                }
            }
        }

        public void VoegStudentMetCursussenToe(Student student)
        {
            string sql1 = @"insert into student (naam, klasid) output inserted.ID values(@naam, @klasid)";
            string sql2 = @"insert into cusus_to_student(cususid, studentid) values(@cursusid, @studentid)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand command1 = connection.CreateCommand())
            using(SqlCommand command2 = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    command1.Transaction = trans;
                    command2.Transaction = trans;
                    command1.CommandText = sql1;
                    command1.Parameters.AddWithValue("@naam", student.Naam);
                    command1.Parameters.AddWithValue("@klasid", student.KlasId);
                    int newStudentid = (int)command1.ExecuteScalar();
                    command2.CommandText = sql2;
                    command2.Parameters.AddWithValue("@studentid", newStudentid);
                    command2.Parameters.Add(new SqlParameter("@cursusid", System.Data.SqlDbType.Int));
                    foreach( Cursus c in student.cursussen)
                    {
                        Console.WriteLine("aa");
                        command2.Parameters["@cursusid"].Value = c.Id;
                        command2.ExecuteNonQuery();
                    }
                    trans.Commit();
                }catch (Exception ex)
                {
                    trans.Rollback();
                }
            }

        }
    }
}
