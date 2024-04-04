namespace Core.Application.DTOs;

public interface IBasePaginationFilter
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}