using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Domain.Database.Entities;

namespace URLShortener.DAL.Repository
{
    public class UrlShortenerRepository : IUrlShortenerRepository
    {
        /// <summary>
        /// Получение всех созданных ссылков текущего пользователя
        /// </summary>
        /// <param name="userGuid">гуид пользователя</param>
        /// <returns></returns>
        public async Task<List<UserUrl>> GetUserUrlAsync(Guid userGuid)
        {
            using (var context = new UrlShortenerContext())
            {
                return  await (context.Users.Where(p => p.UserGuid == userGuid)?.Join(context.UserUrls,
                    p => p.Id, userUrl => userUrl.UserId, (p, userUrl) => userUrl)).ToListAsync();
            }
        }

        /// <summary>
        /// Получить оригинальную ссылку по сжатой
        /// </summary>
        /// <param name="compactUrl">сжатая ссылка</param>
        /// <returns></returns>
        public async Task<string> GeOriginUrlByCompactUrlAsync(string compactUrl)
        {
            using (var context = new UrlShortenerContext())
            {
                var result = await (context.UserUrls.FirstOrDefaultAsync(x => x.CompactUrl == compactUrl));
                return result?.OriginUrl;
            }
        }
        /// <summary>
        /// Инкремент кол-во переходов по ссылке
        /// </summary>
        /// <param name="compactUrl">ключ компактной ссылки</param>
        /// <returns></returns>
        public async Task IncrementNumberOfViewAsync(string compactUrl)
        {
            using (var context = new UrlShortenerContext())
            {
                var userUrl = await context.UserUrls.FirstOrDefaultAsync(x => x.CompactUrl == compactUrl);
                if (userUrl != null)
                {
                    userUrl.NumberOfViews += 1;
                    await context.SaveChangesAsync();
                }
            }
        }
        /// <summary>
        /// Создание сжатой ссылки
        /// </summary>
        /// <param name="userUrl"><seealso cref="UserUrl"/></param>
        /// <param name="userGuid">гуид пользователя</param>
        /// <returns></returns>
        public async Task<string> AddUserUrlAsync(UserUrl userUrl, Guid userGuid)
        {
            using (var context = new UrlShortenerContext())
            {
                if(userUrl == null || userGuid == Guid.Empty)
                    throw new NullReferenceException();

                //есть такой пользователь в Бд?
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserGuid == userGuid);
                var userId = user?.Id;
                if (userId == null)
                {
                    //добавляем в БД пользователя
                    var result = context.Users.Add(new User() {UserGuid = userGuid});
                    userId = result.Id;
                }

                userUrl.UserId = (long)userId;

                var newUrl = context.UserUrls.Add(userUrl);
                await context.SaveChangesAsync();

                return newUrl.CompactUrl;
            }
        }
    }
}
