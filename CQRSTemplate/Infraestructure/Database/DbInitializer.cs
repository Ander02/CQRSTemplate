using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SQRSTemplate.Infraestructure.Database
{
    public class DbInitializer
    {
        public static async Task Initialize(Db db, ILogger<Startup> logger)
        {
            await db.SaveChangesAsync();
        }
    }
}
