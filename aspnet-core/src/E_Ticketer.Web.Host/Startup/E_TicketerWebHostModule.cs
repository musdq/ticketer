using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using E_Ticketer.Configuration;

namespace E_Ticketer.Web.Host.Startup
{
    [DependsOn(
       typeof(E_TicketerWebCoreModule))]
    public class E_TicketerWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public E_TicketerWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(E_TicketerWebHostModule).GetAssembly());
        }
    }
}
