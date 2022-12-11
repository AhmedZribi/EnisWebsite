namespace EnisProject.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public byte[]? Timetable { get; set; }
        public IList<ApplicationUser>? Students { get; set; }
    }
}
