using Microsoft.EntityFrameworkCore;

namespace PsttTask.Domain.Contracts
{
    public class EFSpecification
    {
        public DbContext Context { get; private set; }

        public EFSpecification(DbContext context)
        {
            Context = context;
        }
    }
}
