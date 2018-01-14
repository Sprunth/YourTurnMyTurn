using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace YourTurnMyTurn.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", o => "Hello World");
            Get("/test/{name}", o => new TestPerson() {Name = o.name});
        }
    }

    public class TestPerson
    {
        public string Name { get; set; }
    }
}
