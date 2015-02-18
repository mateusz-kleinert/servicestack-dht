using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;

namespace server.Services
{
    [Route("/dht")]
    [Route("/dht/{Key}")]
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
        public object Post(Dht request)
        {
			if (request.Key != "") {
				server.Program.node.Insert(new KeyValuePair<String, String> (request.Key,request.Value));
            	return new DhtBoolResponse {Result = true};
			}
			return new DhtBoolResponse {Result = false};
        }

		public object Get (Dht request)
		{
			if (request.Key != "") {
				return new DhtResponse {Result = server.Program.node.Find(request.Key)};
			}
			return new DhtResponse {Result = null};
        }

		public object Delete(Dht request)
        {
			server.Program.node.Delete(request.Key);
			return new DhtBoolResponse {Result = true};
        }
    }
}

