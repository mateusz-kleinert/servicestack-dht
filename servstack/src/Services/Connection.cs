using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System;
using ServiceStack.Text;
using servstack;

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
		public object Any(Connection request)
		{
			return new ConnectionResponse {Primary = new Range(1, 1), Secondary = new Range(1, 1), Data = new List<Tuple<String, String>> ()};
		}
	}
}

