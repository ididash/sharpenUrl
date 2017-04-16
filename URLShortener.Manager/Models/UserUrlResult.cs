using System;

namespace URLShortener.Manager.Models
{
    public class UserUrlResult
    {
        /// <summary>
        /// гуид пользователя
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// сжатая ссылка
        /// </summary>
        public string CompactUrl { get; set; }
    }
}
