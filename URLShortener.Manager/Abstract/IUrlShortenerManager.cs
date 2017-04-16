using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortener.Manager.Models;

namespace URLShortener.Manager.Abstract
{
    public interface IUrlShortenerManager
    {
        Task<List<UserUrlData>> GetUserUrlAsync(Guid userGuid);

        Task<string> GeOriginUrlByCompactUrlAsync(string compactUrl);

        Task IncrementNumberOfViewAsync(string compactUrl);

        Task<UserUrlResult> AddUserUrlAsync(UserUrlData userUrl);
    }
}
