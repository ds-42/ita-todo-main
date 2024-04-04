using System.Reflection;

namespace Core.ArchitectureTests.Utils;

public static class PropertyExtensions
{
    public static bool IsInitOnly(this PropertyInfo property)
    {
        if (!property.CanWrite)
        {
            return false;
        }
 
        var setMethod = property.SetMethod;
        var setMethodReturnParameterModifiers = setMethod!.ReturnParameter.GetRequiredCustomModifiers();
        return setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
    }
}