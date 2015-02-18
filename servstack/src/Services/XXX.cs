using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace server.Services
{
    [Route("/api/trololo")]
	[Route("/api/trololo/{Name}/{Age}")]
    public class Trololo: IReturn<TrololoResponse>
    {
        public string Name { get; set; }
		public int Age { get; set; }
    }

    public class TrololoResponse
    {
        public string Result { get; set; }
    }

    public class TrololoService: Service
    {
        public object Any(Trololo request)
        {
            return new TrololoResponse {Result = "Hello, " + request.Name + request.Age};
        }
    }
}

