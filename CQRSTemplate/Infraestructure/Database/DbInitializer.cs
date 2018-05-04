using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CQRSTemplate.Util;

namespace CQRSTemplate.Infraestructure.Database
{
    public class DbInitializer
    {
        public static async Task Initialize(Db db)
        {
            await db.SaveChangesAsync();
        }
    }
}
