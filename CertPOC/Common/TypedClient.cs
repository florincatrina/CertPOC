using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CertPOC.Common
{
    public class TypedClient
    {
        public HttpClient Client { get; private set; }

        public TypedClient(HttpClient Client)
        {
            this.Client = Client;
        }


    }
}
