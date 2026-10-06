using GrigoriqzErshovKt_31_23.Interfaces.DisciplinesInterfaces;
using GrigoriqzErshovKt_31_23.Interfaces.GradesInterfaces;
using GrigoriqzErshovKt_31_23.Interfaces.GroupsInterfaces;
using GrigoriqzErshovKt_31_23.Interfaces.StudentsInterfaces;

namespace GrigoriqzErshovKt_31_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();

            return services;
        }
    }
}
