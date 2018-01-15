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
            Get("/", o => "YourTurnMyTurn");
        }
    }
}
