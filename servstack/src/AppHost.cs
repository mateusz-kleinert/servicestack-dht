using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;


namespace server
{
    public class AppHost : AppHostHttpListenerBase
    {
        private readonly bool m_debugEnabled = true;
        
        
        public AppHost ()
            : base ("Server HttpListener", typeof (AppHost).Assembly)
        {
        }
        
        
        public override void Configure (Container container)
        {
			var config = new EndpointHostConfig ();
            
            if (m_debugEnabled)
            {
                config.DebugMode = true; //Show StackTraces in service responses during development
                config.WriteErrorsToResponse = true;
                config.ReturnsInnerException = true;
            }
            
            SetConfig (config);
        }
        
        
        private void CreateMissingTables (Container container)
        {
            var authRepo = (OrmLiteAuthRepository) container.Resolve<IUserAuthRepository> ();
            authRepo.CreateMissingTables ();
        }
    }
}