using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Model_9H;

namespace Api_9H.Controllers
{
    public class BaseController : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            HttpRequestMessage req = controllerContext.Request;

            string requestUrl = req.RequestUri.AbsoluteUri;

            string requestBody = req.Content.ReadAsAsync<string>().Result;

            Task<HttpResponseMessage> execute = base.ExecuteAsync(controllerContext, cancellationToken);

            RESTfulModel resp = execute.Result.Content.ReadAsAsync<RESTfulModel>().Result;

            string responseBody = JsonConvert.SerializeObject(resp);
            
            return execute;
        }
    }
}
