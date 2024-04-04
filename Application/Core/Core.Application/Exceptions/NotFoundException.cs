using System.Text.Json;

namespace Core.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(object filter) : base("Not found by filter: " + JsonSerializer.Serialize(filter))
    {
    }

    public NotFoundException(string message, object filter) : base(message + " Filter: " +
                                                                    JsonSerializer.Serialize(filter))
    {
    }
}