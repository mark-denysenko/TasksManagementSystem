using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.TestAggregate;

namespace Tasks.Infrastructure
{
    internal class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000).HasDefaultValue("");
            builder.Property(x => x.TaskStatus).IsRequired().HasDefaultValue(TaskStatus.Planned);
            builder.Property(x => x.StartDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.FinishDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(x => x.SubTasks).WithOne(x => x.ParentTask).OnDelete(DeleteBehavior.NoAction);

        }
    }
}