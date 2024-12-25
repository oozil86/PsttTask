namespace PsttTask.Domain.Contracts;

public record PageList<T>
{
    public List<T> Data { get; set; }
    public int Count { get; set; }

    public PageList(List<T> data, int count)
    {
        Data = data;
        Count = count;
    }
}
