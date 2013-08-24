using Nancy;

namespace TestNancy
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };
        }
    }
}