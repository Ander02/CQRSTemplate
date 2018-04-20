using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CQRSTemplate.Util;

namespace CQRSTemplate.Database
{
    public class DbInitializer
    {
        public static async Task Initialize(Db db, ILogger<Startup> logger)
        {
            await db.SaveChangesAsync();
        }
    }
}
