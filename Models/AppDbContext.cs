using Microsoft.EntityFrameworkCore;

namespace Skillora.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<Student> Students {  get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobConstraint> JobConstraints { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SkillJob>()
                .HasKey(sj=>new {sj.SkillId, sj.JobId});
            modelBuilder.Entity<SkillJob>()
                .HasOne(sj => sj.Job).WithMany(j => j.SkillJobs).HasForeignKey(sj => sj.JobId);
            modelBuilder.Entity<SkillJob>()
                .HasOne(sj=>sj.Skill).WithMany(s=>s.SkillJobs).HasForeignKey(sj => sj.SkillId);


            modelBuilder.Entity<SkillStudent>()
                .HasKey(ss => new { ss.SkillId, ss.StudentId });
            modelBuilder.Entity<SkillStudent>()
                .HasOne(ss => ss.Student).WithMany(s => s.SkillStudents).HasForeignKey(ss => ss.StudentId);
            modelBuilder.Entity<SkillStudent>()
                .HasOne(ss => ss.Skill).WithMany(s => s.SkillStudents).HasForeignKey(ss => ss.SkillId);


            modelBuilder.Entity<StudentJob>()
                .HasKey(sj=>new {sj.StudentId, sj.JobId});
            modelBuilder.Entity<StudentJob>()
                .HasOne(sj => sj.Student).WithMany(s => s.StudentJobs).HasForeignKey(sj => sj.StudentId);
            modelBuilder.Entity<StudentJob>()
                .HasOne(sj => sj.Job).WithMany(j => j.StudentJobs).HasForeignKey(sj => sj.JobId);
        }
    }
}
