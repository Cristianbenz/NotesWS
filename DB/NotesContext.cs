using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class NotesContext : DbContext
    {
        public NotesContext() { }
        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {
        }

        public virtual DbSet<Note> Note { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}