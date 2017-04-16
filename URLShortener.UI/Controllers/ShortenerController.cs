using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using URLShortener.Manager.Abstract;
using URLShortener.Manager.Models;
using URLShortener.UI.Helpers;

namespace URLShortener.UI.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly IUrlShortenerManager _urlShortenerManager;

        public ShortenerController(IUrlShortenerManager urlShortenerManager)
        {
            this._urlShortenerManager = urlShortenerManager;
        }

        /// <summary>
        /// Архив сжатых ссылок
        /// </summary>
        /// <param name="userGuid">гуид пользователя, ИД из Бд не показываем на UI</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetShortenerArchive(Guid? userGuid)
        {
            List<UserUrlData> result = new List<UserUrlData>();
            try
            {
                if (userGuid == null)
                    return Json(new List<UserUrlData>(), JsonRequestBehavior.AllowGet);

                string domain = GetDomain();
                if (string.IsNullOrEmpty(domain))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(null);
                }
                //в бд хранится только ключ сокращенной ссылки без домена, т.к. сайт 
                //может переехать на другой адрес (или поддомен), чтобы не обновлять потом БД под новый домен
                result = await _urlShortenerManager.GetUserUrlAsync((Guid) userGuid);
                result?.ForEach(x => x.CompactUrl = domain + x.CompactUrl);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                //записать в лог ошибку 
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Добавляем в БД новый url
        /// </summary>
        /// <param name="data">Описание URL для добавления <seealso cref="UserUrlData"/></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ConvertOrignUrl(UserUrlData data)
        {
            try
            {
                if (data == null || string.IsNullOrWhiteSpace(data.OriginUrl))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(null);
                }
                var compactUrl = CompactUrl.GenerateCompactUrl();
                //todo compactUrl не уникальный, надо добавить проверку что такого еще не было в БД
                data.CompactUrl = compactUrl;
                //todo можно создать несколько разных компактных ссылок на один и тот же уникальный
                var result = await _urlShortenerManager.AddUserUrlAsync(data);
                if(result == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(null);
                }
                result.CompactUrl = GetDomain() + result.CompactUrl;
                return Json(result);

            }
            catch
            {
                //записать в лог ошибку 
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(null);
            }
        }

        private string GetDomain()
        {
            var uri = HttpContext.Request.Url;
            if (uri == null)
            {
                return null;
            }
            var domain = uri.Scheme + "://" + uri.Host + "/";
            return domain;
        }
    }
}