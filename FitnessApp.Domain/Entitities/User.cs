using System.ComponentModel.DataAnnotations;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Security;

namespace FitnessApp.Domain.Entitities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;

    private static IUserRepository _userRepository;

    public User(){}
    
    public User(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Register(string userName, string password)
    {
        var hash = SecurityProvider.HashPasword(password, out var salt);
        var user = new User(_userRepository);
        user.UserName = userName;
        user.PasswordSalt = Convert.ToHexString(salt);
        user.PasswordHash = hash;
        _userRepository.Save(user);
    } 

    public bool GetCredentialsAreValid(string userName, string password)
    {
        var user = _userRepository.GetUser(userName);
        return SecurityProvider.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
    }
}