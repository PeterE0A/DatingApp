namespace DatingApp.Services
{
    public interface IDatabaseService
    {
        bool CreateAccount(string username, string passwordHash);
        bool DeleteAccount(int userId);
        bool CreateProfile(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode);
        bool DeleteProfile(int userId);
        bool Login(string username, string passwordHash);
        bool Logout();
        bool AddLike(int likerUserId, int likedUserId);
        bool CheckForMatch(int userId1, int userId2);
        // Add other methods for search, messaging, etc.
    }
}
