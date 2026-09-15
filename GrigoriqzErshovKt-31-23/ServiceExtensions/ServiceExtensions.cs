using GrigoriqzErshovKt_31_23.Interfaces.StudentsInterfaces;

namespace GrigoriqzErshovKt_31_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}