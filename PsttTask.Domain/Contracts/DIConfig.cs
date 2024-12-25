using System.Reflection;

namespace PsttTask.Domain.Contracts;

public class DIConfig
{
    public string NameSpace { get; set; } = "PsttTask";
    public Assembly[] Assemblies { get; set; }
}
