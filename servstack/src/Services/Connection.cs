using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System;
using ServiceStack.Text;

namespace server.Services
{
	[Route("/connection")]
	public class Connection: IReturn<ConnectionResponse>
	{
		public int Port { get; set; }
	}

	public class ConnectionResponse
	{
		public int primaryLowerBound { get; set; }
		public int primaryUpperBound { get; set; }
		public int secondaryLowerBound { get; set; }
		public int secondaryUpperBound { get; set; }
		public List<Tuple<String, String>> Data { get; set; } 
	}

	public class ConnectionService: Service
	{
		public object Any(Connection request)
		{
			return new ConnectionResponse {primaryLowerBound = 10, primaryUpperBound = 1, 
				secondaryLowerBound = 1, secondaryUpperBound = 1, Data = new List<Tuple<String, String>> ()};
		}
	}
}

