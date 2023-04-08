using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.EntityConfiguration
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration() {
            Property(m => m.GenreId).IsRequired();
            Property(m=>m.Name).IsRequired();
            Property(m=>m.ReleaseDate).IsRequired();
            Property(m => m.GenreId).IsRequired();
            Property(m=>m.Stocks).IsRequired();
        }
    }
}