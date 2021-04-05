using CertPOC.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CertPOC.Controllers
{


    public class TestController : Controller
    {
        private readonly string testUrl = "https://self-signed.badssl.com/";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TypedClient _typedClient;

        public TestController(IHttpClientFactory httpClientFactory, TypedClient typedClient)
        {
            _httpClientFactory = httpClientFactory;
            _typedClient = typedClient;
        }


        public async Task<IActionResult> Index()
        {
            var result = new TestResult();

            try
            {
                await _httpClientFactory.CreateClient().GetAsync(testUrl);
                result.DefaultClientResult = "Ok";
            }
            catch(Exception ex1)
            {
                result.DefaultClientResult = ex1.Message;
            }

            try
            {
                await _httpClientFactory.CreateClient("namedClient").GetAsync(testUrl);
                result.NamedClientResult = "Ok";
            }
            catch (Exception ex1)
            {
                result.NamedClientResult = ex1.Message;
            }

            try
            {
                await _typedClient.Client.GetAsync(testUrl);
                result.TypedClientResult = "Ok";
            }
            catch (Exception ex1)
            {
                result.TypedClientResult = ex1.Message;
            }


            return Json(result);
        }

        public class TestResult
        {
            public string DefaultClientResult { get; set; } 

            public string NamedClientResult { get; set; }

            public string TypedClientResult { get; set; }

        }


        
    }
}
