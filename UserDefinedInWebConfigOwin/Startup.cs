using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Routing;
using Owin;
using UserDefinedInWebConfig;

namespace UserDefinedInWebConfigOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            app.Use((owinContext, next) =>
            {
                if (ConfigurationManager.AppSettings["test-security"] != "true")
                    return next.Invoke();

                var username = ConfigurationManager.AppSettings["username"];
                var roles = ConfigurationManager.AppSettings["roles"].Split(' ');

                ClaimsIdentity identity = new ClaimsIdentity(authenticationType: "test-security");
                identity.AddClaim(new Claim(ClaimTypes.Name, username));
                roles.ToList().ForEach(role => identity.AddClaim(new Claim(ClaimTypes.Role, role)));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                //HttpContext.Current.User = principal; //this will work as well if you are hosting in IIS, 
                                                        //but if you are using owin, might as well use the owin 
                                                        //to set the principal

                owinContext.Authentication.User = principal;
                
                return next.Invoke();
            });

            //rest of your owin startup configuration
        }
    }
}