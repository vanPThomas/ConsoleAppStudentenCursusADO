namespace ConsoleAppStudentenCursusADO
{
    public class Klas
    {
        public Klas(string klasnaam, string lokaal)
        {
            this.klasnaam = klasnaam;
            this.lokaal = lokaal;
        }

        public int id { get; set; }
        public string klasnaam { get; set; }
        public string lokaal { get; set; }
    }
}
