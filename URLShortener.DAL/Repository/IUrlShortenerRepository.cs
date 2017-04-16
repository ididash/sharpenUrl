using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortener.Domain.Database.Entities;

namespace URLShortener.DAL.Repository
{
    public interface IUrlShortenerRepository
    {
        Task<List<UserUrl>> GetUserUrlAsync(Guid userGuid);

        Task<string> GeOriginUrlByCompactUrlAsync(string compactUrl);

        Task IncrementNumberOfViewAsync(string compactUrl);

        Task<string> AddUserUrlAsync(UserUrl userUrl, Guid userGuid);
    }
}
