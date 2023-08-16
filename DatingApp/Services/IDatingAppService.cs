using System.Threading.Tasks;

namespace DatingApp.Services
{
    public interface IDatingAppService
    {
        Task<bool> CreateAccountAsync(string username, string password);
        Task<bool> DeleteAccountAsync(int userId);
        Task<bool> CreateProfileAsync(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode);
        Task<bool> DeleteProfileAsync(int userId);
        Task<bool> AddLikeAsync(int likerUserId, int likedUserId);
        Task<bool> CheckForMatchAsync(int userId1, int userId2);
    }
}
