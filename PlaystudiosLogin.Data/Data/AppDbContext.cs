using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaystudiosLogin.Api.Models;

namespace PlaystudiosLogin.Api.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext() : base()
        {
        }
        public AppDbContext(DbContextOptions options)
        : base(options)
        {
        }
    }
}
