using System;
using ClaimsSubmission.Domain.AggregatesModel.BatchAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClaimsSubmission.Infrastructure.Persistence
{
	public class BatchesContext : DbContext
	{
		public BatchesContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Batch> Batches { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Batch>(entity =>
				{
					entity.ToTable("batches");

					entity.HasKey(i => i.Id);
					entity.Property(i => i.Id)
						.HasColumnName("id")
						.ValueGeneratedNever();

					entity.Property(i => i.BatchUri)
						.HasColumnName("batch_uri")
						.HasMaxLength(2000)
						.IsRequired();

					entity.Property(i => i.SubmissionFeedbackUri)
						.HasColumnName("submission_feedback_uri")
						.HasMaxLength(2000);

					entity.Property(i => i.VettingReportUri)
						.HasColumnName("vetting_report_uri")
						.HasMaxLength(2000);

					entity.Property(i => i.CreationDate)
						.HasColumnName("creation_date")
						.IsRequired();
				});

		}
	}
}
