namespace URLShortener.Domain.Database.Entities
{
    public abstract class Entity
    {
        /// <summary>
        /// уникальный идентификатор пользователя
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Признак удаления пользователя (сейчас не используется)
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
