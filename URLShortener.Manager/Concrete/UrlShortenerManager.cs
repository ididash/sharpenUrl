using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.DAL.Repository;
using URLShortener.Domain.Database.Entities;
using URLShortener.Manager.Abstract;
using URLShortener.Manager.Models;

namespace URLShortener.Manager.Concrete
{
    public class UrlShortenerManager : IUrlShortenerManager
    {
        private readonly IUrlShortenerRepository _urlShortenerRep;

        public UrlShortenerManager(IUrlShortenerRepository urlShortenerRep)
        {
            this._urlShortenerRep = urlShortenerRep;
        }
        /// <summary>
        /// Получение всех созданных ссылков текущего пользователя
        /// </summary>
        /// <param name="userGuid">гуид пользователя</param>
        /// <returns></returns>
        public async Task<List<UserUrlData>> GetUserUrlAsync(Guid userGuid)
        {
            var result = await _urlShortenerRep.GetUserUrlAsync(userGuid);
            if(result == null)
                return new List<UserUrlData>();

            return result.Select(x => new UserUrlData()
            {
                UserId = x.UserId,
                CompactUrl = x.CompactUrl,
                NumberOfViews = x.NumberOfViews,
                OriginUrl = x.OriginUrl,
                CreateOn = x.CreateOn
            }).ToList();
        }

        /// <summary>
        /// Инкремент кол-во переходов по ссылке
        /// </summary>
        /// <param name="compactUrl">ключ компактной ссылки</param>
        /// <returns></returns>
        public async Task IncrementNumberOfViewAsync(string compactUrl)
        {
            await _urlShortenerRep.IncrementNumberOfViewAsync(compactUrl);
        }

        /// <summary>
        /// Создание сжатой ссылки
        /// </summary>
        /// <param name="userUrl">Описание добавляемой ссылки<seealso cref="UserUrlData"/></param>
        /// <returns></returns>
        public async Task<UserUrlResult> AddUserUrlAsync(UserUrlData userUrl)
        {
            if (userUrl.UserGuid == null || userUrl.UserGuid == Guid.Empty)
            {
                userUrl.UserGuid = Guid.NewGuid();
            }
            var data = new UserUrl
            {
                CreateOn = DateTime.Now,
                CompactUrl = userUrl.CompactUrl,
                NumberOfViews = 0,
                OriginUrl = userUrl.OriginUrl
            };
            var compactUrl = await _urlShortenerRep.AddUserUrlAsync(data, (Guid)userUrl.UserGuid);
            return new UserUrlResult { CompactUrl = compactUrl, UserGuid = (Guid)userUrl.UserGuid };
        }

        /// <summary>
        /// Получить оригинальную ссылку по сжатой
        /// </summary>
        /// <param name="compactUrl">сжатая ссылка</param>
        /// <returns></returns>
        public async Task<string> GeOriginUrlByCompactUrlAsync(string compactUrl)
        {
            return await _urlShortenerRep.GeOriginUrlByCompactUrlAsync(compactUrl);
        }
    }
}
