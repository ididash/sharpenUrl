using System;

namespace URLShortener.Domain.Database.Entities
{
    public class UserUrl : Entity
    {
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
        public virtual User User { get; set; }
    }
}
