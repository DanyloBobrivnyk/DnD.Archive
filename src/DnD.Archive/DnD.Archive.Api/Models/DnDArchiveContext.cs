using Microsoft.EntityFrameworkCore;

namespace DnD.Archive.Api.Models
{
    public class DnDArchiveContext : DbContext
    {
        public DnDArchiveContext(DbContextOptions<DnDArchiveContext> options) : base(options)
        {
        }

        public DbSet<Specialization> Specializations { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Character> Characters { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasKey(s => s.Name);
            modelBuilder.Entity<Skill>().HasKey(s => s.Id);
            modelBuilder.Entity<Character>().HasKey(c => c.Id);

            // Many-to-many relationship between Skill and Specialization
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.AllowedSpecialization)
                .WithMany(s => s.Skills)
                .UsingEntity(j => j.ToTable("SkillSpecializations"));

            // One-to-many relationship between Character and Specialization
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Specialization)
                .WithMany()
                .HasForeignKey(c => c.SpecializationName)
                .HasPrincipalKey(s => s.Name);

            // Many-to-many relationship between Character and Skill
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Skills)
                .WithMany(s => s.Characters)
                .UsingEntity(j => j.ToTable("CharacterSkills"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
