using MIS333K_Team11_FinalProjectV2.Models;
//using MIS333K_Team11_FinalProjectV2.DAL;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    public class GenreData
    {
        public void SeedGenres(AppUser.AppDbContext db)
        {
            Genre gen1 = new Genre();
            gen1.GenreName = "Action";
            db.Genres.AddOrUpdate(g => g.GenreName, gen1);
            db.SaveChanges();

            Genre gen2 = new Genre();
            gen2.GenreName = "Adventure";
            db.Genres.AddOrUpdate(g => g.GenreName, gen2);
            db.SaveChanges();

            Genre gen3 = new Genre();
            gen3.GenreName = "Animation";
            db.Genres.AddOrUpdate(g => g.GenreName, gen3);
            db.SaveChanges();

            Genre gen4 = new Genre();
            gen4.GenreName = "Comedy";
            db.Genres.AddOrUpdate(g => g.GenreName, gen4);
            db.SaveChanges();

            Genre gen5 = new Genre();
            gen5.GenreName = "Crime";
            db.Genres.AddOrUpdate(g => g.GenreName, gen5);
            db.SaveChanges();

            Genre gen6 = new Genre();
            gen6.GenreName = "Drama";
            db.Genres.AddOrUpdate(g => g.GenreName, gen6);
            db.SaveChanges();

            Genre gen7 = new Genre();
            gen7.GenreName = "Family";
            db.Genres.AddOrUpdate(g => g.GenreName, gen7);
            db.SaveChanges();

            Genre gen8 = new Genre();
            gen8.GenreName = "Fantasy";
            db.Genres.AddOrUpdate(g => g.GenreName, gen8);
            db.SaveChanges();

            Genre gen9 = new Genre();
            gen9.GenreName = "History";
            db.Genres.AddOrUpdate(g => g.GenreName, gen9);
            db.SaveChanges();

            Genre gen10 = new Genre();
            gen10.GenreName = "Horror";
            db.Genres.AddOrUpdate(g => g.GenreName, gen10);
            db.SaveChanges();

            Genre gen11 = new Genre();
            gen11.GenreName = "Musical";
            db.Genres.AddOrUpdate(g => g.GenreName, gen11);
            db.SaveChanges();

            Genre gen12 = new Genre();
            gen12.GenreName = "Romance";
            db.Genres.AddOrUpdate(g => g.GenreName, gen12);
            db.SaveChanges();

            Genre gen13 = new Genre();
            gen13.GenreName = "Science Fiction";
            db.Genres.AddOrUpdate(g => g.GenreName, gen13);
            db.SaveChanges();

            Genre gen14 = new Genre();
            gen14.GenreName = "Thriller";
            db.Genres.AddOrUpdate(g => g.GenreName, gen14);
            db.SaveChanges();

            Genre gen15 = new Genre();
            gen15.GenreName = "War";
            db.Genres.AddOrUpdate(g => g.GenreName, gen15);
            db.SaveChanges();

            Genre gen16 = new Genre();
            gen16.GenreName = "Western";
            db.Genres.AddOrUpdate(g => g.GenreName, gen16);
            db.SaveChanges();
        }
    }
}
