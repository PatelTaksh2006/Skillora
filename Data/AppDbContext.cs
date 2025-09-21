using Microsoft.EntityFrameworkCore;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;

namespace Skillora.Data
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
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Job)
                .WithOne(j => j.Company)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Job>()
    .HasOne(j => j.JobConstraint)
    .WithOne(jc => jc.Job)
    .HasForeignKey<JobConstraint>(jc => jc.JobId)
    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SkillJob>()
                .HasKey(sj=>new {sj.SkillId, sj.JobId});
            modelBuilder.Entity<SkillJob>()
                .HasOne(sj => sj.Job).WithMany(j => j.SkillJobs).HasForeignKey(sj => sj.JobId).OnDelete(DeleteBehavior.Cascade); ;
            modelBuilder.Entity<SkillJob>()
                .HasOne(sj=>sj.Skill).WithMany(s=>s.SkillJobs).HasForeignKey(sj => sj.SkillId).OnDelete(DeleteBehavior.Cascade); ;


            modelBuilder.Entity<SkillStudent>()
                .HasKey(ss => new { ss.SkillId, ss.StudentId });
            modelBuilder.Entity<SkillStudent>()
                .HasOne(ss => ss.Student).WithMany(s => s.SkillStudents).HasForeignKey(ss => ss.StudentId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SkillStudent>()
                .HasOne(ss => ss.Skill).WithMany(s => s.SkillStudents).HasForeignKey(ss => ss.SkillId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<StudentJob>()
                .HasKey(sj=>new {sj.StudentId, sj.JobId});
            modelBuilder.Entity<StudentJob>()
                .HasOne(sj => sj.Student).WithMany(s => s.StudentJobs).HasForeignKey(sj => sj.StudentId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StudentJob>()
                .HasOne(sj => sj.Job).WithMany(j => j.StudentJobs).HasForeignKey(sj => sj.JobId).OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Skillora.Models.ViewModels.DeleteJobViewModel> DeleteJobViewModel { get; set; }
        public DbSet<Skillora.Models.ViewModels.CompanyViewModel> CompanyViewModel { get; set; }
        //public DbSet<Skillora.Models.ViewModels.DeleteStudentViewModel> DeleteStudentViewModel { get; set; }
        //public DbSet<Skillora.Models.ViewModels.EditJobViewModel> EditJobViewModel { get; set; }
        //public DbSet<Skillora.Models.ViewModels.EditStudentViewModel> EditStudentViewModel { get; set; }
        //public DbSet<StudentViewModel> StudentViewModel { get; set; }
        //public DbSet<Skillora.Models.ViewModels.CreateJobViewModel> CreateJobViewModel { get; set; }
    }
}
