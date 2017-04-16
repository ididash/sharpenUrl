using System.Threading.Tasks;
using System.Web.Mvc;
using URLShortener.Manager.Abstract;

namespace URLShortener.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlShortenerManager _urlShortenerManager;

        public HomeController(IUrlShortenerManager urlShortenerManager)
        {
            this._urlShortenerManager = urlShortenerManager;
        }

        /// <summary>
        /// Главная и страница архива, все что не прописано в роуте, пойдет в ShortenerUrl
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.File("/src/app/index.html", "text/html");
        }

        /// <summary>
        /// Редирект на оригинальный сайт + инкремент переходов
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ShortenerUrl()
        {
            var currentPath = Url.RequestContext.HttpContext.Request.CurrentExecutionFilePath;

            if(string.IsNullOrWhiteSpace(currentPath))
                return this.File("/src/app/index.html", "text/html");

            currentPath = currentPath.Replace("/", string.Empty);

            //ищем в БД. если не найдем кидаем на свой сайт
            var orignUrl = await _urlShortenerManager.GeOriginUrlByCompactUrlAsync(currentPath);
            if(string.IsNullOrWhiteSpace(orignUrl))
                return this.File("/src/app/index.html", "text/html");

            await _urlShortenerManager.IncrementNumberOfViewAsync(currentPath);

            return Redirect(orignUrl);
        }
    }
}