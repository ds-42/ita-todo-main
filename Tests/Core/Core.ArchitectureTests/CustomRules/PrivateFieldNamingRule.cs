using Mono.Cecil;
using NetArchTest.Rules;

namespace Core.ArchitectureTests.CustomRules;

internal class PrivateFieldNamingRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        var fields = type.Fields
            .Where(f => f.IsPrivate && !f.IsInitOnly && !f.IsStatic)
            .Where(f => f.CustomAttributes.All(attr => attr.AttributeType.Name != "CompilerGeneratedAttribute"))
            .ToArray();

        return fields.Length == 0 || fields.All(f => f.Name.StartsWith("_"));
    }
}