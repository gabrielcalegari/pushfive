using PushFive.Catalog.Domain.Models;
using PushFive.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushFive.Catalog.Domain.Repository
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<Song> GetSongById(Guid id);

        Task<IEnumerable<Song>> GetSongs(int page, int size);

        Task<long> CountSongs();
    }
}
