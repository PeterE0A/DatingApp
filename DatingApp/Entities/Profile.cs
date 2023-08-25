namespace DatingApp.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        private string? FullName { get; set; }
        private DateTime Birthday { get; set; }
        private string? Gender { get; set; }
        private string? City { get; set; }
        private string? PostalCode { get; set; }


    }
}
