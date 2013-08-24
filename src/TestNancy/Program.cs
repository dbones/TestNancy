using System.Collections;
using Topshelf;

namespace TestNancy
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                cfg =>
                {
                    cfg.Service<App>(
                        srv =>
                        {
                            srv.ConstructUsing(app => new App());
                            srv.WhenStarted(app => app.Start());
                            srv.WhenStopped(app => app.Stop());
                        });

                    cfg.RunAsLocalSystem();
                    cfg.SetServiceName("NancyWebServer");
                    cfg.SetDisplayName("NancyWebServer");
                });
        }
    }
}
