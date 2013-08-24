using System;
using Nancy.Hosting.Self;

namespace TestNancy
{
    public class App
    {
        NancyHost _nancyHost;

        public void Start()
        {
            _nancyHost = new NancyHost(new CustomBootstrapper(), new Uri("http://localhost:1234"));
            _nancyHost.Start();
        }

        public void Stop()
        {
            _nancyHost.Stop();
            _nancyHost.Dispose();
        }

    }
}