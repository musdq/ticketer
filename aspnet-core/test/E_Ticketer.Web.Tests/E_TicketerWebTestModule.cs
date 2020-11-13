using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using E_Ticketer.EntityFrameworkCore;
using E_Ticketer.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace E_Ticketer.Web.Tests
{
    [DependsOn(
        typeof(E_TicketerWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class E_TicketerWebTestModule : AbpModule
    {
        public E_TicketerWebTestModule(E_TicketerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(E_TicketerWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(E_TicketerWebMvcModule).Assembly);
        }
    }
}