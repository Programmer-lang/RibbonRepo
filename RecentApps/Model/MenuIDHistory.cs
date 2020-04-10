namespace RecentApps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuIDHistory")]
    public partial class MenuIDHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public decimal? AppNum { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        public string Note { get; set; }
    }
}
