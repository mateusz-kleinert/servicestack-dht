using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;

namespace server.Services
{
    [Route("/dht", "POST")]
    [Route("/dht/{Key}", "GET")]
    public class Dht: IReturn<DhtResponse>
    {
        public string Key { get; set; }
		public string Value { get; set; }
    }

    public class DhtResponse
    {
        public KeyValuePair<String, String> Result { get; set; }
    }

	public class DhtBoolResponse
	{
		public bool Result { get; set; }
	}

    public class DhtService: Service
    {
		public DhtBoolResponse Post(Dht request)
        {
			return new DhtBoolResponse {Result = server.Program.node.Insert(new KeyValuePair<String, String> (request.Key,request.Value))};
        }

		public object Get (Dht request)
		{
			return new DhtResponse {Result = server.Program.node.Find(request.Key)};
        }

		public object Delete(Dht request)
        {
			Console.WriteLine(request.Key+"?");
			return new DhtBoolResponse {Result = server.Program.node.Delete(request.Key)};
        }
    }
}

