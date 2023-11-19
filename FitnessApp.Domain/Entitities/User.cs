using System.ComponentModel.DataAnnotations;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Security;

namespace FitnessApp.Domain.Entitities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    
    public ICollection<SportActivity> SportActivities { get; set; } = new List<SportActivity>();

    private static IUserRepository _userRepository;

    public User(){}
    
    public User(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddActivity(SportActivity activity)
    {
        SportActivities.Add(activity);
    } 
    
    public void Register(string userName, string password)
    {
        var hash = SecurityProvider.HashPasword(password, out var salt);
        var user = new User(_userRepository)
        {
            UserName = userName,
            PasswordSalt = Convert.ToHexString(salt),
            PasswordHash = hash
        };
        _userRepository.AddUser(user);
    }

    public User? GetUser(int userId)
    {
        return _userRepository.GetUserById(userId);
    }

    public bool GetCredentialsAreValid(string userName, string password)
    {
        try
        {
            var user = _userRepository.GetUser(userName);
            if (SecurityProvider.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                UserName = userName;
                Id = user.Id;
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public void SaveOrUpdate()
    {
        _userRepository.SaveOrUpdate();
    }
}