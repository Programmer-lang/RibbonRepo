namespace RecentApps
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.ObjectModel;

    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual ObservableCollection<MenuIDHistory> MenuIDHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuIDHistory>()
                .Property(e => e.AppNum)
                .HasPrecision(15, 10);

            modelBuilder.Entity<MenuIDHistory>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<MenuIDHistory>()
                .Property(e => e.Note)
                .IsUnicode(false);
        }
    }
}
