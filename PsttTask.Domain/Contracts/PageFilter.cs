using PsttTask.Domain.Enums;

namespace PsttTask.Domain.Contracts;

public record PageFilter
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public OrderType OrderType { get; set; }
}
