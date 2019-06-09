using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DistributedCaching.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Threading.Tasks;

namespace DistributedCaching.Controllers
{
    public class HomeController : Controller
    {
        private static string cacheKey = "cachedTimeUTC";

        private readonly IDistributedCache _cache;

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Cache()
        {
            var cachedTime = "No cache found";

            var encodedCachedTimeUTC = await _cache.GetAsync("cachedTimeUTC");

            if (encodedCachedTimeUTC != null)
            {
                cachedTime = Encoding.UTF8.GetString(encodedCachedTimeUTC);
            }

            return new ContentResult() { Content = cachedTime };
        }
    }
}
