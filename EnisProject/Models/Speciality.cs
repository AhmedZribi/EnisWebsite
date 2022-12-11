namespace EnisProject.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ApplicationUser>? Users { get; set; }
        public IList<Internship>? Internships { get; set; }
    }
}
