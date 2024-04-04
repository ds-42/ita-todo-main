using AutoFixture.Xunit2;
using Core.Tests.Helpers;

namespace Core.Tests.Attributes;

public class FixtureAutoDataAttribute : AutoDataAttribute
{
    public FixtureAutoDataAttribute() : base(() => FixtureHelper.DefaultFixture)
    {
    }
}