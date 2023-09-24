
# Asp.net core

<details>
<summary>What is the entry point of the asp.net core application? </summary>
The main method in the program.cs file basically the entry point for the application. The main method contains code like. Then all other configuration is done in startup class.

```csharp
      public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
      }
      public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args).
            ConfigureWebHostDefaults(x => x.UseStartup <Startup> ());
      }
```
The Startup class is the entry point to the application, setting up configuration and wiring up services the application will use. 
Developers configure a request pipeline in the Startup class that is used to handle all requests made to the application.
The Startup class can optionally accept dependencies in its constructor that are provided through dependency injection.
Typically, the way an application will be configured is defined within its Startup class’s constructor (see Configuration).
The Startup class must define a Configure method, and may optionally also define a ConfigureServices method, which will be called when the application is started.
## The Configure Method
The configure method must accept **IApplicationBuilder** argument. It can also accept **IHostingEnvironment** and **ILoggerFactory**. 
We can configure pipeline using extension methods for IApplicationBuilder.
```csharp
  app.UseDeveloperExceptionPage();
  app.UseDatabaseErrorPage();
  app.UseExceptionHandler("/Home/Error");
  app.UseStaticFiles();
  app.UseIdentity();
  app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
    });
```
Each Use extension method adds middleware to the request pipeline.
## The ConfigureServices Method
The ConfigureServices method is a public method on your Startup class that takes an IServiceCollection instance as a parameter and optionally returns an IServiceProvider. The ConfigureServices method is called before Configure. This is important, because some features like ASP.NET MVC require certain services to be added in ConfigureServices before they can be wired up to the request pipeline.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    services.AddMvc();

    // Add application services.
    services.AddTransient<IEmailSender, AuthMessageSender>();
    services.AddTransient<ISmsSender, AuthMessageSender>();
}
```
## Services Available in StartUp
### IApplicationBuilder
Used to build the application request pipeline. Available only to the Configure method in Startup. Learn more about Request Features.
### IHostingEnvironment
Provides the current EnvironmentName, ContentRootPath, WebRootPath, and web root file provider. Available to the Startup constructor and Configure method.
### ILoggerFactory
Provides a mechanism for creating loggers. Available to the Startup constructor and Configure method. Learn more about Logging.
### IServiceCollection
The current set of services configured in the container. Available only to the ConfigureServices method, and used by that method to configure the services available to an application.

Different methods and available services
**Startup Constructor** - IApplicationEnvironment - IHostingEnvironment - ILoggerFactory \n
**ConfigureServices** - IServiceCollection \n
**Configure** - IApplicationBuilder - IApplicationEnvironment - IHostingEnvironment - ILoggerFactory
</details>
<details>
<summary>What is difference between Use and Run in middleware?</summary>
Middleware are executed in the same order in which they are added. The difference is, middleware defined using app.Use may call next middleware component in the pipeline. On the other hand, middlware defined using app.Run will never call subsequent middleware
</details>
<details>
<summary>what is difference between Action filter and Middleware</summary>
Middleware can be used for the entire request pipeline but Filters is only used within the Routing Middleware where we have an MVC pipeline so Middleware operates at the level of ASP.NET Core but Filters executes only when requests come to the MVC pipeline.
Middleware will be executed irrespective of the Controller or Action Method we are hitting but Filters will be executed based on which Controller or Action Method it has been configured.
The Execution of Middleware occurs before MVC Context becomes available in the pipeline.
Filters are only part of MVC Middleware but middlewares are part of every request pipeline.
Middleware has access to HttpContext but Filter has access to wider MVC Context which helps us to access routing data and model binding information.
</details>
<details>
<summary>
How asp.net core applications convert from console to Web Application?</summary>
Each asp .net core application will host their own server **Kestral**. HostBuilder.CreateBuilder() does some basic setup of the asp net core platform based on quite a few defaults and conventions. It also setup the kestral server.
The Kestral server is builtin cross-platform server that will actually run as part of the application and handle all the requests.

CreateBuilder() -> Kestral Server -> Configure IIS Integration -> Specify Content Root -> Read Application Settings from IConfiguration

Request -> Kestral Server -> middleware -> endpoint middleware.
</details>

<details>
<summary>How we can implement global exception handling in asp.net core.</summary>
The UseExceptionHandler middleware is a built-in middleware that we can use to handle exceptions globally.

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  
{  
    app.UseExceptionHandler("/Home/Error");  
    app.UseMvc();  
} 
```
```csharp
app.UseExceptionHandler(  
                options =>  
                {  
                    options.Run(  
                        async context =>  
                        {  
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  
                            context.Response.ContentType = "text/html";  
                            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();  
                            if (null != exceptionObject)  
                            {  
                                var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message} </b> {exceptionObject.Error.StackTrace}";  
                                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);  
                            }  
                        });  
                }  
            );  
```



</details>

<details>
<summary>How many method in startup.cs</summary>
There mainly two methods and one constructor in startup.cs

1. **Configure** In this method apps middleware pipeline are configured. This method is called after configure services
2. **ConfigureServices** In this method mainly services are registerd to IServiceCollection which then may be injected to required Controllers and other application classes.
</details>

<details>
<summary> What are service lifetimes in .NET Core?</summary>
Service lifetime means the scope of Injected Service Object.

1. **Singleton**: Instances of services registered with a this lifetime are created once throughout the application life.
1. **Scoped**: Instances of services registered with a this lifetime are created for every http request.
1. **Transient**: Instances of services registered with a this lifetime are created every time that their injection into a class is required.

</details>

<details>
<summary>What is a Kestrel?</summary>
Kestrel is a cross-platform web server for ASP.NET Core. Kestrel is the web server that's included and enabled by default in ASP.NET Core project templates.
</details>

<details>
<summary>how can we implement CORS in asp.net core?</summary>
CORS means Cross Origin Requests that means allow browsers to load resources from other origins
It can be configured in three ways.
The call to UseCors must be placed after UseRouting, but before UseAuthorization

- In middleware using a named policy or default policy.
```csharp
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://www.contoso.com");
                      });
});
----
app.UseCors(MyAllowSpecificOrigins);
----
```
- Using endpoint routing.
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/echo",
        context => context.Response.WriteAsync("echo"))
        .RequireCors(MyAllowSpecificOrigins);

    endpoints.MapControllers()
             .RequireCors(MyAllowSpecificOrigins);

    endpoints.MapGet("/echo2",
        context => context.Response.WriteAsync("echo2"));

    endpoints.MapRazorPages();
});
```
- With the [EnableCors] attribute.
```csharp
[Route("api/[controller]")]
[ApiController]
public class WidgetController : ControllerBase
{
    // GET api/values
    [EnableCors("AnotherPolicy")]
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] { "green widget", "red widget" };
    }

    // GET api/values/5
    [EnableCors("Policy1")]
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
        return id switch
        {
            1 => "green widget",
            2 => "red widget",
            _ => NotFound(),
        };
    }
}
```
</details>

<details>
<summary>Contentnegotiation in webapi?</summary>
The process of selecting the best representation for a given response when there are multiple representations available.
The primary mechanism for content negotiation in HTTP are these request headers: <br>

**Accept**: Which media types are acceptable for the response, such as "application/json," "application/xml," or a custom media type such as "application/vnd.example+xml"\
**Accept-Charset**: Which character sets are acceptable, such as UTF-8 or ISO 8859-1.\
**Accept-Encoding**: Which content encodings are acceptable, such as gzip.\
**Accept-Language**: The preferred natural language, such as "en-us".

First, the pipeline gets the **IContentNegotiator** service from the **HttpConfiguration** object. It also gets the list of media formatters from the **HttpConfiguration.Formatters** collection.

Next, the pipeline calls **IContentNegotiator.Negotiate**, passing in:

- The type of object to serialize
- The collection of media formatters
- The HTTP request

The **Negotiate** method returns two pieces of information:

- Which formatter to use
- The media type for the response

If no formatter is found, the Negotiate method returns null, and the client receives HTTP error 406 (Not Acceptable).

</details>



<details>
<summary>Difference between webapi Routing and mvc routing?</summary>
ASP.NET MVC offers two approaches to routing:

- The route table, which is a collection of routes that can be used to match incoming requests to controller actions.
- Attribute routing, which performs the same function but is achieved by decorating the actions themselves, rather than editing a global route table.

### Conventional routing
With conventional routing, you set up one or more conventions that will be used to match incoming URLs to endpoints in the app. In ASP.NET Core, these endpoints may be controller actions, like in ASP.NET MVC or Web API. The endpoints could also be Razor Pages, Health Checks, or SignalR hubs. All of these routable features are configured in a similar fashion using endpoints:

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthz").RequireAuthorization();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
```

### Attribute routing
Attribute routing in ASP.NET Core is the preferred approach for configuring routing in controllers. If you're building APIs, the [ApiController] attribute should be applied to your controllers. Among other things, this attribute requires the use of attribute routing for actions in such controller classes.

Attribute routing in ASP.NET Core behaves similarly in ASP.NET MVC and Web API. In addition to supporting the [Route] attribute, however, route information can also be specified as part of the HTTP method attribute:

```csharp
[HttpGet("api/products/{id}")]
public async ActionResult<Product> Details(int id)
{
    // ...
}
```

</details>

<details>
<summary>Versioning in web api?</summary>

Using AddApiVersioning Services which are defined in Microsoft.AspNetCore.Mvc.Versioning.

```csharp
public void ConfigureServices(IServiceCollection services)  
{  
    services.AddControllers();  
    services.AddApiVersioning(x =>  
    {  
        x.DefaultApiVersion = new ApiVersion(1, 0);  
        x.AssumeDefaultVersionWhenUnspecified = true;  
        x.ReportApiVersions = true;  
    });  
}  
```
Then to specify the version on use attribute [ApiVersion("1.0")]  

```csharp
    [ApiController]  
    [ApiVersion("1.0")]  
    [Route("api/employee")]  
    public class EmployeeV1Controller : ControllerBase  
    {  
        [HttpGet]  
        public IActionResult Get()  
        {  
            return new OkObjectResult("employees from v1 controller");  
        }  
    }  
```
Api version can be passed using QueryString, as part of route(url) and in HttpHeader 
</details>


<details>
<summary>Mvc application life cycle?</summary>

HttpRequest -> MiddleWares -> Routing -> Controller initialization -> Action Method Execution -> Result Execution -> Returned Data Result Or Strart View Rendering -> Response

### Action Method Execution Steps

Authorization Filter -> Controller Creation -> Model Binding -> Action Filters -> Action Method Execution -> Action Filters -> 

</details>

<details>
<summary>what is container in DI?</summary>
A DI Container is a framework to create dependencies and inject them automatically when required. It automatically creates objects based on the request and injects them when required. DI Container helps us to manage dependencies within the application in a simple and easy way.
</details>

<details>
<summary>How to maintain state in web api?</summary>
State in web api can be managed on client side using cookies in-memory. Then this infomation can be passed to server using cookies, paramaters, headers etc. 
</details> 


<details>
<summary>how to access one method if authorize has used controller level?</summary>
You can add [Authorize] To the controller class, and then add [AllowAnonymous] to the single action you don't want to be authorized. Example:

```csharp
[Authorize]
public class AccountController : Controller
{
    public ActionResult Profile()
    {
        return View();
    }
    [AllowAnonymous]
    public ActionResult Login()
    {
        return View();
    }
}
```
</details>
