using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using System;

namespace server.Services
{
    [Route("/replica")]
    public class Replica: IReturn<ReplicaResponse>
    {
        public int Hn { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }
    }

    public class ReplicaResponse
    {
        public KeyValuePair<String, String> Result { get; set; }
    }

	public class ReplicaBoolResponse
	{
		public bool Result { get; set; }
	}

    public class ReplicaService: Service
    {
        public object Post(Replica request)
        {
            return new ReplicaBoolResponse {Result = server.Program.node.Insert(new KeyValuePair<String, String> (request.Key,request.Value),request.Hn)};
        }

		public object Get(Replica request)
        {
            return new ReplicaResponse {Result = server.Program.node.Find(request.Key,request.Hn)};
        }

		public object Delete(Replica request)
        {
            return new ReplicaBoolResponse {Result = server.Program.node.Delete(request.Key,request.Hn)};
        }
    }
}

