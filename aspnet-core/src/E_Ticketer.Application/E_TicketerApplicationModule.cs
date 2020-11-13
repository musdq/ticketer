using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using E_Ticketer.Authorization;

namespace E_Ticketer
{
    [DependsOn(
        typeof(E_TicketerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class E_TicketerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<E_TicketerAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(E_TicketerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
