using System.Threading.Tasks;
using DatingApp.Data;

namespace DatingApp.Services
{
    public interface IDatingAppService
    {
        Task<bool> LogoutAsync();
        Task<bool?> LogInAsync(string username, string password);
       // Task<bool> VerifyCredentialsAsync(string username, string password);
        Task<bool> CreateAccountAsync(string username, string password);
        //Task<bool> DeleteAccountAsync(string username, string password);
        Task<bool> DeleteAccountAsync(int userId);
        Task<bool> CreateProfileAsync(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode);
        Task<bool> DeleteProfileAsync(int userId);
        Task<bool> AddLikeAsync(int likerUserId, int likedUserId);
        Task<bool> CheckForMatchAsync(int userId1, int userId2);
    }
}
