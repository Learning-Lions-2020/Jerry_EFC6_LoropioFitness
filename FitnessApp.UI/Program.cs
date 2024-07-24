using FitnessApp.Data.DBContext;
using FitnessApp.Data.Repository;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using FitnessApp.UI.Dialog;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<ISportEventRepository, SportEventRepository>()
    .AddScoped<User>()
    .AddDbContext<FitnessAppContext>()
    .BuildServiceProvider();

    new AuthenticationDialog(serviceProvider).Start(); 