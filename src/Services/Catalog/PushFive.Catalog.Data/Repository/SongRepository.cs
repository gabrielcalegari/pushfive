using Microsoft.EntityFrameworkCore;
using PushFive.Catalog.Domain.Models;
using PushFive.Catalog.Domain.Repository;
using PushFive.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushFive.Catalog.Data.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly CatalogContext catalogContext;
        public IUnitOfWork UnitOfWork => catalogContext;

        public SongRepository(CatalogContext catalogContext)
        {
            this.catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<IEnumerable<Song>> GetSongs(int page, int size)
        {
            return await catalogContext.Songs.AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Genre)
                .Skip((page-1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<long> CountSongs()
        {
            return await catalogContext.Songs.LongCountAsync();
        }

        public async Task<Song> GetSongById(Guid id)
        {
            return await catalogContext.Songs.FindAsync(id);
        }

        public async Task<IEnumerable<Song>> GetSongsByIds(params Guid[] ids)
        {
            if (ids.Length > 20)
                throw new NotSupportedException("Maximum number of ids are 20");

            var songIdList = ids.ToList();

            return await catalogContext.Songs
                .Where(song => songIdList.Contains(song.Id))
                .Include(s => s.Artist)
                .Include(s => s.Genre)
                .ToListAsync();
        }

        public void Dispose()
        {
            catalogContext?.Dispose();
        }
    }
}
