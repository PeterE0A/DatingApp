namespace DatingApp.Entities
{
    public class Profile
    {
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }

        public bool IsLiked { get; set; }
    }
}
