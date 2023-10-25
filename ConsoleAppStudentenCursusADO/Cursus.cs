namespace ConsoleAppStudentenCursusADO
{
    public class Cursus
    {
        public Cursus(string naam, int klasId)
        {
            Naam = naam;
            KlasId = klasId;
        }

        public Cursus(int id, string naam, int klasId)
        {
            Id = id;
            Naam = naam;
            KlasId = klasId;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int KlasId { get; set; }
    }
}
