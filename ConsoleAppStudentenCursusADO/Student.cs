namespace ConsoleAppStudentenCursusADO
{
    public class Student
    {
        public Student(string naam, int klasId)
        {
            Naam = naam;
            KlasId = klasId;
        }

        public Student(int id, string naam, int klasId)
        {
            Id = id;
            Naam = naam;
            KlasId = klasId;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int KlasId { get; set; }
        public List<Cursus> cursussen { get; set; } = new List<Cursus>();

    }
}
