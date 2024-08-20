namespace FitnessApp.API
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<SportActivityDTO> SportActivities { get; set; } = new List<SportActivityDTO>();
    }
}
