using System;

namespace URLShortener.Manager.Models
{
    public class UserUrlData
    {
        /// <summary>
        /// гуид пользователя, чтобы не показывать ИД на клиенте
        /// </summary>
        public Guid? UserGuid { get; set; }
        /// <summary>
        /// ИД клиента
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Оригинальная ссылка
        /// </summary>
        public string OriginUrl { get; set; }
        /// <summary>
        /// Сжатая ссылка (без домена)
        /// </summary>
        public string CompactUrl { get; set; }
        /// <summary>
        /// Дата создания ссылки
        /// </summary>
        public DateTime CreateOn { get; set; }
        /// <summary>
        /// Кол-во переходов на сайт по сжатой ссылке
        /// </summary>
        public long NumberOfViews { get; set; }
    }
}
