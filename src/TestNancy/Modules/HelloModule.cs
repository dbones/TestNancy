using Nancy;

namespace TestNancy
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = parameters =>
            {
                string name = "Dave"; // <- uber model 
                return View["index", name];
            };
        }
    }
}