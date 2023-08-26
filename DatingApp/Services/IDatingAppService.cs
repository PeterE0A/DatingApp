using System.Threading.Tasks;
using DatingApp.Data;
using DatingApp.Entities;

namespace DatingApp.Services
{
    public interface IDatingAppService
    {
        //-----------------------------------------
        Task<int?> GetUserIdByUsername(string username);
        //---------------------------------------
        Task<List<Profile>> GetProfilesByGender(string gender);


        List<Profile> GetProfiles();
        Profile GetProfile(int id);
        Task<bool> DeleteProfileAsync(int userId);

        Task<bool> CheckForProfileAsync(string username);

        Task<bool> LogoutAsync();
        Task<bool?> LogInAsync(string username, string password);
        Task<bool> CreateAccountAsync(string username, string password);
        Task<bool> DeleteAccountAsync(int userId);
        Task<bool> CreateProfileAsync(string fullName, DateTime birthday, string gender, string city, string postalCode);
        
        Task<bool> AddLikeAsync(int likerUserId, int likedUserId);
        Task<bool> CheckForMatchAsync(int userId1, int userId2);
    }
}
