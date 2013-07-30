namespace Colegios.Web.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Distance { get; set; }
        public string TypeSchool { get; set; }

    }
}