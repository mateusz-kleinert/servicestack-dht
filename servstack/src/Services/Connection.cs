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
	[Route("/connection", "POST")]
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
			string ip = iHttpListenerRequest.RemoteEndPoint.ToString().Split(':')[0];

			var results = Program.node.ChildCreate(ip + ":" + request.Port.ToString());
			return new ConnectionResponse {Primary = results.Item1, Secondary = results.Item2, Data = results.Item3};
		}
	}
}

