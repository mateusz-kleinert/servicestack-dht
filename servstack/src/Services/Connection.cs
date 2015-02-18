using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System;
using ServiceStack.Text;
using servstack;
using server;
using System.Net;

namespace server.Services
{
	[Route("/connection")]
	public class Connection: IReturn<ConnectionResponse>
	{
		public int Port { get; set; }
	}

	public class ConnectionResponse
	{
		public Range Primary { get; set; }
		public Range Secondary { get; set; }
		public List<Tuple<String, String>> Data { get; set; } 
	}

	public class ConnectionService: Service
	{
		public object Post(Connection request)
		{
			HttpListenerRequest iHttpListenerRequest = (HttpListenerRequest)base.RequestContext.Get<IHttpRequest>().OriginalRequest;

			string IP = iHttpListenerRequest.RemoteEndPoint.ToString().Split(':')[0];

			Program.node.childrens.Add (new Tuple<String, Range, Range> (IP + ":" + request.Port.ToString(), new Range(1, 1), new Range(1, 1)));
			Console.Out.WriteLine (Program.node.childrens.ToJson());
			return new ConnectionResponse {Primary = new Range(1, 1), Secondary = new Range(1, 1), Data = new List<Tuple<String, String>> ()};
		}
	}
}

