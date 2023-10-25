namespace ConsoleAppStudentenCursusADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=HIMEKO\SQLEXPRESS; Initial Catalog=studentenCursussen; Integrated Security=True";
            Databeheer db = new Databeheer(connectionString);

            List<Cursus> cursussen = new List<Cursus>();
            cursussen = db.GeefCursussen();
            foreach(Cursus cursus in cursussen)
            {
                Console.WriteLine(cursus.Naam);
            }

            Student student = new Student("joske", 2);
            student.cursussen = cursussen;
            //student.cursussen.Add(cursussen[0]);
            //student.cursussen.Add(cursussen[1]);
            //student.cursussen.Add(cursussen[2]);

            db.VoegStudentMetCursussenToe(student);
        }

       
    }
}