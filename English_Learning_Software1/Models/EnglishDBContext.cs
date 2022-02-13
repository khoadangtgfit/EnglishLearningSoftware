using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace English_Learning_Software1.Models
{
    public partial class EnglishDBContext : DbContext
    {
        public EnglishDBContext()
            : base("name=EnglishDBContext1")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Ownere> Owneres { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz_Detail> Quiz_Detail { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Story_Detail> Story_Detail { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Test_Detail> Test_Detail { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Video_Detail> Video_Detail { get; set; }
        public virtual DbSet<Vocabulary> Vocabularies { get; set; }
        public virtual DbSet<Vocabulary_Type> Vocabulary_Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Password)
                .IsUnicode(false);

            modelBuilder.Entity<Answer>()
                .Property(e => e.Answer_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Ownere>()
                .Property(e => e.Ownere_PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Ownere>()
                .Property(e => e.Ownere_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Ownere>()
                .HasMany(e => e.Quiz_Detail)
                .WithRequired(e => e.Ownere)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ownere>()
                .HasMany(e => e.Story_Detail)
                .WithRequired(e => e.Ownere)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ownere>()
                .HasMany(e => e.Test_Detail)
                .WithRequired(e => e.Ownere)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ownere>()
                .HasMany(e => e.Video_Detail)
                .WithRequired(e => e.Ownere)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Question_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .Property(e => e.Story_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .Property(e => e.Story_EN_Content)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .Property(e => e.Story_Audio)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .HasMany(e => e.Story_Detail)
                .WithRequired(e => e.Story)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .Property(e => e.Test_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Test_Detail)
                .WithRequired(e => e.Test)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.Video_Path)
                .IsUnicode(false);

            modelBuilder.Entity<Video>()
                .HasMany(e => e.Video_Detail)
                .WithRequired(e => e.Video)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vocabulary>()
                .Property(e => e.English_Vocabulary)
                .IsUnicode(false);

            modelBuilder.Entity<Vocabulary_Type>()
                .Property(e => e.Vocabulary_Type_EN_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Vocabulary_Type>()
                .HasMany(e => e.Quiz_Detail)
                .WithRequired(e => e.Vocabulary_Type)
                .WillCascadeOnDelete(false);
        }
    }
}
