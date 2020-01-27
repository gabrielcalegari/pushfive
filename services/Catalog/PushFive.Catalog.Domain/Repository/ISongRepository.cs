using PushFive.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushFive.Catalog.Domain.Repository
{
    public interface ISongRepository : IDisposable
    {
        Task<Song> GetSongById(Guid id);

        Task<IEnumerable<Song>> GetSongs(int page, int size);

        Task<long> CountSongs();
    }
}
