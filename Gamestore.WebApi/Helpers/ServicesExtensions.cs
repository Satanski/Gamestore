using System.Reflection;

namespace Gamestore.WebApi.Helpers;

public static class ServicesExtensions
{
    public static void AddInstallersFromAssemblyContaining<TMarker>(this IServiceCollection services)
    {
        AddInstallersFromAssembliesContaining(services, typeof(TMarker));
    }

    public static void AddInstallersFromAssembliesContaining(this IServiceCollection services, params Type[] assemblyMarkers)
    {
        var assemblies = assemblyMarkers.Select(x => x.Assembly).ToArray();
        AddInstallersFromAssemblies(services, assemblies);
    }

    public static void AddInstallersFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var installerTypes = assembly.DefinedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            var installers = installerTypes.Select(Activator.CreateInstance).Cast<IInstaller>();

            foreach (var installer in installers.OrderBy(x => x.Order))
            {
                installer.AddServices(services);
            }
        }
    }

    public static void AddInstallersFromAssemblies(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var installerTypes = assembly.DefinedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            var installers = installerTypes.Select(Activator.CreateInstance).Cast<IInstaller>();

            foreach (var installer in installers.OrderBy(x => x.Order))
            {
                installer.AddServices(services);
            }
        }
    }
}
