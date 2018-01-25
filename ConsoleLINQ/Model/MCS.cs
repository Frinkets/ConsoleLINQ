namespace ConsoleLINQ.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MCS : DbContext
    {
        public MCS()
            : base("name=MCS")
        {
        }

        public virtual DbSet<TrackServiceHistory> TrackServiceHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
