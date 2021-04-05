using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CertPOC
{
    // based on : https://github.com/dotnet/extensions/blob/release/3.1/src/HttpClientFactory/Http/src/DefaultHttpMessageHandlerBuilder.cs
    // configures ServerCertificateCustomValidationCallback on PrimaryHandler to always return true 

    public class CertHttpMessageHandlerBuilder : HttpMessageHandlerBuilder
    {

        public CertHttpMessageHandlerBuilder()
        {
            var ignoreSslHanlder = new HttpClientHandler();
            ignoreSslHanlder.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            
            PrimaryHandler = ignoreSslHanlder;
        }

        public override HttpMessageHandler Build()
        {
            if (PrimaryHandler == null)
            {
                var message = string.Format("The '{0}' must not be null.", nameof(PrimaryHandler));
                throw new InvalidOperationException(message);
            }

            return HttpMessageHandlerBuilder.CreateHandlerPipeline(PrimaryHandler, AdditionalHandlers);
        }

        public override IList<DelegatingHandler> AdditionalHandlers { get; } = new List<DelegatingHandler>();
        public override string Name { get; set; }
        public override HttpMessageHandler PrimaryHandler { get; set; }
    }
}
