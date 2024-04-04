using AutoFixture.Xunit2;

namespace Core.Tests.Attributes;

public class FixtureInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public FixtureInlineAutoDataAttribute(params object[] values) : base(new FixtureAutoDataAttribute(), values)
    {
    }
}