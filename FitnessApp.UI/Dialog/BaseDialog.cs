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
                var user = ServiceProvider.GetService<User>();

                if (user != null)
                {
                    return user;
                }

            }
            return null;
        }

        public BaseDialog(ServiceProvider serviceProvider) 
        {
            ServiceProvider = serviceProvider;
        }
    }
}
