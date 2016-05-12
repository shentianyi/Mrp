using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WMSPlugIn.WmsRest
{
    public class WmsRestClient
    {
        private string host;
        

        public WmsRestClient(string host) {
            this.host = host;
        }

        public IRestRequest GetRequest(string url, Method method = Method.GET, DataFormat format = DataFormat.Json) {
            var req = new RestRequest(url, method);
            req.RequestFormat = format;
            return req;
        }


        public IRestResponse Execute(IRestRequest request)
        {
            var response = genClient().Execute(request);
            return responseHandler(response);
        }


        private RestClient genClient()
        {
            var client = new RestClient();
            client.Timeout = 1000000000;
            client.BaseUrl = this.host;
          
            return client;
        }

        private IRestResponse responseHandler(IRestResponse res)
        {
            try
            {
                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WebFaultException<string>("无权限访问系统，请重新登陆", HttpStatusCode.Unauthorized);
                }
                else if (res.StatusCode != HttpStatusCode.OK && res.StatusCode != HttpStatusCode.Created)
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = res.StatusCode;
                    WebOperationContext.Current.OutgoingResponse.StatusDescription = res.StatusDescription;
                    throw new WebFaultException<string>(res.StatusDescription, res.StatusCode);
                }
                return res;
            }
            catch (WebFaultException<string> we)
            {
                throw new WebFaultException<string>(we.Detail, we.StatusCode);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("网络错误，请检查网络连接", HttpStatusCode.GatewayTimeout);
            }
        }

    }
}
