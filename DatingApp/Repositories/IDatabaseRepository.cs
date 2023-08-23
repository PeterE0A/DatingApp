using System.Threading.Tasks;

namespace DatingApp.Repositories
{
    public interface IDatabaseRepository
    {
        
        Task<bool> CreateAccountAsync(string username, string password);
        Task<string> GetPasswordAsync(string username);
        Task<bool> DeleteAccountAsync(int userId);
        Task<bool> CreateProfileAsync(string fullName, DateTime birthday, string gender, string city, string postalCode);
        //Task<bool> DeleteProfileAsync(int userId);
        Task<bool> DeleteProfileAsync();
        Task<bool> AddLikeAsync(int likerUserId, int likedUserId);
        Task<bool> CheckForMatchAsync(int userId1, int userId2);
    }
}
