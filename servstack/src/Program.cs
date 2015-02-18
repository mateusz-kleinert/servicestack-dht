using System;
using ServiceStack.ServiceClient.Web;
using server.Services;
using System.Collections.Generic;
using servstack;

namespace server
{
    internal class Program
    {
		public static Node node;

        private static void Main (string[] args)
		{
			ushort port;
			if (args.Length == 0)
				port = 8888;
			else
				port = ushort.Parse(args[0]);

			if (args.Length > 1) {
				var client = new JsonServiceClient ("http://" + args[1]);
				ConnectionResponse resp = client.Post(new Connection { Port = port });
				Console.WriteLine("Connected to " + "http://" + args[1]);

				node = new Node (resp.Primary, resp.Secondary, "http://" + args[1], resp.Data);
			} else {
				node = new Node (new Range(Hardcode.PrimaryLowerBound,
				                           Hardcode.PrimaryUpperBound),
				                 new Range(Hardcode.SecondaryLowerBound,
				          				   Hardcode.SecondaryUpperBound),
				                 "",
				                 new List<Tuple<String, String>> ());
			}

            var appHost = new AppHost();
            appHost.Init();

			string listeningOn = string.Format("http://127.0.0.1:{0}/", port);
            appHost.Start(listeningOn);

            Console.WriteLine("AppHost created at {0}, listening on {1}", DateTime.Now, listeningOn);
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}

