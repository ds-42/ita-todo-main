using System.Reflection;
using AutoMapper;
using Core.Application.Abstractions.Mappings;

namespace Core.Application.BaseRealizations;

public abstract class MappingRegister : Profile
{
    protected MappingRegister(Assembly scanAssembly)
    {
        RegisterMappingFromMarker(scanAssembly, typeof(IMapFrom<>), nameof(IMapFrom<object>.CreateMap));
        RegisterMappingFromMarker(scanAssembly, typeof(IMapTo<>), nameof(IMapTo<object>.CreateMap));
    }

    private void RegisterMappingFromMarker(
        Assembly scanAssembly,
        Type marker,
        string markerCreateMapMethodName
    )
    {
        var types = scanAssembly
            .GetExportedTypes()
            .Where(t => t.IsClass && (t.IsPublic || t.IsNested) && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == marker))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            if (instance == null)
            {
                throw new Exception($"Activator can't create instance of {type.Name}");
            }

            var mappings = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == marker);

            foreach (var mapping in mappings)
            {
                var methodInfo = mapping.GetMethod(markerCreateMapMethodName);
                methodInfo!.Invoke(
                    instance,
                    [
                        this
                    ]
                );
            }
        }
    }
}