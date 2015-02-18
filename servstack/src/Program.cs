using System;
using ServiceStack.ServiceClient.Web;
using server.Services;

namespace server
{
    internal class Program
    {
        private static void Main (string[] args)
		{
			if (args.Length > 1) {
				Console.WriteLine("ZUOOOO");
				var client = new JsonServiceClient("http://127.0.0.1:8889/");
				HelloResponse resp = client.Get(new Hello { Name = "World" });
				Console.WriteLine(resp.Result);
			}
            var appHost = new AppHost();
            appHost.Init();
			ushort port;
			if (args.Length == 0)
            	port = 8888;
			else
				port = ushort.Parse(args[0]);
            string listeningOn = string.Format("http://*:{0}/", port);
            appHost.Start(listeningOn);

            Console.WriteLine("AppHost created at {0}, listening on {1}", DateTime.Now, listeningOn);
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}

