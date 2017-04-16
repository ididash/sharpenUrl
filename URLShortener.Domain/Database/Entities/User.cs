using System;
using System.Collections.Generic;

namespace URLShortener.Domain.Database.Entities
{
    public class User : Entity
    {
        private ICollection<UserUrl> _userUrl;
        /// <summary>
        /// гуид пользователя
        /// </summary>
        public Guid UserGuid { get; set; }
        public virtual ICollection<UserUrl> UserUrls => _userUrl ?? (_userUrl = new List<UserUrl>());
    }
}
