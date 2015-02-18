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
			String ip = "127.0.0.1";
			ushort port = 8888;

			if (args.Length > 1) {
				ip = args [0];
				port = ushort.Parse (args [1]);
			}

			if (args.Length > 2) {
				try {
					var client = new JsonServiceClient ("http://" + args[2]);
					ConnectionResponse resp = client.Post(new Connection { Port = port });
					Console.WriteLine("Connected to " + "http://" + args[2]);
					node = new Node (resp.Primary, resp.Secondary, "http://" + args[2], resp.Data);
				} catch (Exception) {
					Console.WriteLine("Cannot connect to " + "http://" + args[2]);
					return;
				}
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

			string listeningOn = string.Format("http://{0}:{1}/", ip, port);
            appHost.Start(listeningOn);

			Console.WriteLine("AppHost created at {0}, listening on {1}", DateTime.Now, listeningOn);
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}

