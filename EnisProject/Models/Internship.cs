namespace EnisProject.Models
{
	public class Internship
	{
        public int InternshipId { get; set; }
        public string InternshipHeader { get; set; }
        public string InternshipInfo { get; set; }
        public byte[] InternshipPicture { get; set; }
        public string InternshipLocation { get; set; }
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
    }
}
