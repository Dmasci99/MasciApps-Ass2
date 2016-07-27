namespace MasciApps_Ass2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;

    public class MusicStoreEntities : DbContext
    {
        public MusicStoreEntities() :
            base("name=MusicStoreConnection")
        {

        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
    }
}