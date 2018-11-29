using AppInterfaces.Infrastructure;
using AppInterfaces.Interfaces;
using AppInterfaces.Repository;
using Ninject.Modules;
using Ninject.Web.Common;
using RepositoryInfrastructure;
using RepositoryLayer;
using RepositoryLayer.Repositories;


namespace ServiceLayer
{
    public class DependencyResolver : NinjectModule
    {
        public override void Load()
        {
            //Bind<IContextFactory<DbContext>>()
            //    .ToConstructor(ctxFac => new DBContextFactory
            //        (ConfigurationManager.ConnectionStrings["AzureDBContext"].ConnectionString));
            Bind<IEntitiesContext>().To<AzureDBContext>().InRequestScope();
            Bind<IAppUnitOfWork>().To<AppUnitOfWork>().InRequestScope();
            Bind<IActionAccessPermissionRepository>().To<ActionAccessPermissionRepository>().InRequestScope();
            Bind<ILogInfoRepository>().To<LogInfoRepository>().InRequestScope();
            Bind<IModulePermissionRepository>().To<ModulePermissionRepository>().InRequestScope();
            Bind<IModuleRepository>().To<ModuleRepository>().InRequestScope();
            Bind<IPagePermissionRepository>().To<PagePermissionRepository>().InRequestScope();            
            Bind<IRolesRepository>().To<RolesRepository>().InRequestScope();            
            Bind<IScreenPermissionRepository>().To<ScreenPermissionRepository>().InRequestScope();
            Bind<IScreenRepository>().To<ScreenRepository>().InRequestScope();
            Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            Bind<IHomeRepository>().To<HomeRepository>().InRequestScope();
            Bind<IPostRepository>().To<PostRepository>().InRequestScope();
            Bind<IAdminRepository>().To<AdminRepository>().InRequestScope();
            Bind<IManageAdminRepository>().To<ManageAdminRepository>().InRequestScope();
            Bind<IManageUserRepository>().To<ManageUserRepository>().InRequestScope();
            Bind<IManageContentRepository>().To<ManageContentRepository>().InRequestScope();
            Bind<IManageCategoryRepository>().To<ManageCategoryRepository>().InRequestScope();
            Bind<IManageRssFeedRepository>().To<ManageFeedRepository>().InRequestScope();
            Bind<IScheduleRepository>().To<ScheduleRepository>().InRequestScope();
            Bind<IPreferenceRepository>().To<PreferenceRepository>().InRequestScope();
            Bind<IHomeValueRepository>().To<HomeValueRepository>().InRequestScope();
        }
    }
}
