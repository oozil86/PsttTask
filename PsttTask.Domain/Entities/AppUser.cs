using Microsoft.AspNetCore.Identity;

namespace PsttTask.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstNameAr { get; private set; }
        public string SecondNameAr { get; private set; }
        public string LastNameAr { get; private set; }
        public string FirstNameEn { get; private set; }
        public string SecondNameEn { get; private set; }
        public string LastNameEn { get; private set; }
        public Guid Reference { get; private set; }

        public string GetArName()
        => FirstNameAr + LastNameAr;

        public string GetEnName()
        => FirstNameEn + LastNameEn;
    }
}
