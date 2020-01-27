using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PushFive.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PushFive.Catalog.Data
{
    public static class Seeder
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var dataContext = scope.ServiceProvider.GetRequiredService<CatalogContext>())
                {
                    try
                    {
                        RunSeed(dataContext);
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                        logger.LogError(ex, "An error occurred seeding the DB.");
                        
                        throw;
                    }
                }
            }

            return host;
        }

        private static void RunSeed(CatalogContext context)
        {
            if (context.Songs.Any())
                return;

            var songsFromJson = ReadSongsJson();

            var artists = songsFromJson.GroupBy(s => s.Artists).Select(g => new Artist(g.Key)).ToArray();
            context.Artists.AddRange(artists);
            
            var genres = songsFromJson.GroupBy(s => s.Genre).Select(g => new Genre(g.Key)).ToArray();
            context.Genres.AddRange(genres);
            
            var songs = songsFromJson.Select(s =>
            {
                var artist = artists.Single(a => a.Name == s.Artists);
                var genre = genres.Single(g => g.Name == s.Genre);
                return new Song(s.Id, s.Name, artist.Id, genre.Id);
            });

            context.Songs.AddRange(songs);

            context.SaveChanges();
        }

        private static IEnumerable<SongDto> ReadSongsJson()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PushFive.Catalog.Data.spotify-top100-2018.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<SongDto>>(result);
            }
        }

        private class SongDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Artists { get; set; }
            public string Genre { get; set; }
        }
    }
}
