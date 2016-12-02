

using System;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new QueriesContext();

            var actionMovies = context.Videos.Where(m => m.Genre.Name == "Action").OrderBy(m => m.Name);

            //  foreach (var movie in actionMovies)
            //      Console.WriteLine(movie.Name);

            var dramaMovies = context.Videos
                .Where(m => m.Genre.Name == "Drama" && m.Classification == Classification.Gold)
                .OrderByDescending(m => m.ReleaseDate)
                .Where(m => m.Classification == Classification.Gold);

            //  foreach(var movie in dramaMovies)
            //      Console.WriteLine(movie.Name);

            var allMovies = context.Videos
                .Select(m => new {MovieName = m.Name, Genre = m.Name});

            //  foreach (var x in allMovies)
            //      Console.WriteLine("\t {0} ", x.MovieName);

            var allMoviesGroupedByClassification = context.Videos
                .GroupBy(m => m.Classification);

            //  foreach (var groups in allMoviesGroupedByClassification)
            //  {
            //      Console.WriteLine("\t {0}", groups.Key);
            //
            //      foreach (var movie in groups)
            //      {
            //          Console.WriteLine("\t {0}", movie.Name);
            //      }
            //  }

            var groups = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Classification = g.Key.ToString(),
                    Videos = g.OrderBy(v => v.Name)
                });

            //  foreach (var g in groups)
            //  {
            //      Console.WriteLine("Classification: " + g.Classification);
            //
            //      foreach (var v in g.Videos)
            //          Console.WriteLine("\t" + v.Name);
            //  }

            var classifications = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Name = g.Key.ToString(),
                    VideosCount = g.Count()
                })
                .OrderBy(c => c.Name);

            //  foreach (var c in classifications)
            //      Console.WriteLine("{0} ({1})", c.Name, c.VideosCount);

            var genres = context.Genres
                .GroupJoin(context.Videos, g => g.Id, v => v.GenreId, (genre, videos) => new
                {
                    Name = genre.Name,
                    VideosCount = videos.Count()
                })
                .OrderByDescending(g => g.VideosCount);

            //  foreach (var g in genres)
            //      Console.WriteLine("{0} ({1})", g.Name, g.VideosCount);

        }
    }
}
