using FitnessApp.Domain.Entitities;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.UI.Dialog
{
    public class BaseDialog
    {
        protected ServiceProvider? ServiceProvider;

        protected User? LoggedUser;

        public BaseDialog(User user) 
        {
            LoggedUser = user;
        }

        protected User? GetUser()
        {
            if (LoggedUser == null)
            {
                User? user1 = ServiceProvider.GetService<User>();
                User? user = user1;

                if (user != null)
                {
                    return user;
                }

            }
            return LoggedUser;
        }

        public BaseDialog(ServiceProvider serviceProvider) 
        {
            ServiceProvider = serviceProvider;
        }
    }
}
