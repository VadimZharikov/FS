using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FS.BLL.DI
{
    public static class BusinessLogicLayerReg
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            DAL.DI.DataLayerReg.AddDataRepository(services, configuration);
        }
    }
}
