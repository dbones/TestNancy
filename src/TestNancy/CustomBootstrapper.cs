using System.Diagnostics;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using Nancy.ViewEngines;

namespace TestNancy
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StaticConfiguration.EnableRequestTracing = true;

            //see how long a request took
            pipelines.BeforeRequest.AddItemToStartOfPipeline(
                ctx =>
                {
                    Stopwatch timer = Stopwatch.StartNew();
                    ctx.Items.Add("timer", timer);
                    return null;
                });

            pipelines.AfterRequest.AddItemToEndOfPipeline(
                ctx =>
                {
                    if (!ctx.Items.ContainsKey("timer"))
                    {
                        return; //the diagnostics site will not have this key, unsure why
                    }
                    var timer = (Stopwatch)ctx.Items["timer"];
                    timer.Stop();
                    ctx.Trace.TraceLog.WriteLog(log => log.AppendLine(string.Format("Request took: {0} ms", timer.ElapsedMilliseconds)));
                });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //ensure you put the correct assembly in here!
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(HelloModule).Assembly, "TestNancy.Views.Home");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(
                    cfg =>
                    {
                        cfg.ViewLocationProvider = typeof (ResourceViewLocationProvider);
                    });
            }
        }


        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = @"password" }; }
        }
    }
}