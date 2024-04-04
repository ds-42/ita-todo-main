using AutoFixture;
using Castle.Core.Internal;
using Core.Tests.Helpers;
using Xunit.Abstractions;

namespace Core.Tests;

public abstract class TestBase
{
    private readonly Lazy<IFixture> _lazyFixture;
    private readonly ITestOutputHelper _testOutput;

    protected TestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutput = testOutputHelper;
        _lazyFixture = new Lazy<IFixture>(CreateFixture);
    }

    protected IFixture TestFixture => _lazyFixture.Value;

    protected virtual IFixture CreateFixture() => FixtureHelper.DefaultFixture;

    protected void WriteOutput(string message, params object[] args)
    {
        if (args.IsNullOrEmpty())
        {
            _testOutput.WriteLine(message);
        }
        else
        {
            _testOutput.WriteLine(message, args);
        }
    }
}