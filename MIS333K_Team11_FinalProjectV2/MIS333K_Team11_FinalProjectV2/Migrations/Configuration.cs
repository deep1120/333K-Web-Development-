namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MIS333K_Team11_FinalProjectV2.DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MIS333K_Team11_FinalProjectV2.DAL.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            GenreData AddGenres = new GenreData();
            AddGenres.SeedGenres(context);

            MovieData AddMovies = new MovieData();
            AddMovies.SeedMovies(context);

            SeedIdentity si = new SeedIdentity();
            si.AddAdmin(context);
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
