using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Dto;
using Business;
using Core.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace API.Business
{
    public class NebimIntegrationManager : INebimIntegrationService
    {

     
       private readonly ConnectRequestModel _connectConfig;
       public HttpClient RequestClient { get; set; }
       private readonly ILogger<NebimIntegrationManager> _logger;

        public NebimIntegrationManager(IOptions<ConnectRequestModel> connectConfig,HttpClient client,ILogger<NebimIntegrationManager> logger)
        {

            RequestClient = client;
            _connectConfig=connectConfig.Value;
            _logger=logger;
        }
        public T RunNebimProc<T>(ProcedureRequest model, Expression<Func<T, bool>> successPredicate = null) where T : class, new()
        {
            using (_logger.BeginScope(nameof(NebimIntegrationManager) + "." + nameof(RunNebimProc)))
            {
                try
                {
                    var connectionInfo = MakeConnection();

                    if (connectionInfo.StatusCode != 200)
                    {
                        _logger.LogWarning($"Nebim connection error statusCode:{connectionInfo.Status}-{DateTime.UtcNow}");
                        return new T();
                    }

                    var client = new RestClient(_connectConfig.Url);
                    var request = new RestRequest($"/(S({connectionInfo.SessionID}))/IntegratorService/RunProc",
                        Method.POST);
                    request.AddJsonBody(model);
                    var response = client.Execute<T>(request);
                    try
                    {
                        using (_logger.BeginScope("RunNebimProcLoggerScope"))
                        {

                            _logger.LogError($"Nebim Request Json-{Common.ConvertIstanbulDateTime(DateTime.Now):s} :" + Environment.NewLine + JsonConvert.SerializeObject(model,
                                new JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented }) + Environment.NewLine);

                            _logger.LogError($"Nebim Response Json-{Common.ConvertIstanbulDateTime(DateTime.Now):s} :"
                                             + Environment.NewLine + response.Content);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    DisposeConnection(connectionInfo.SessionID);
                    return response.Data;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "NebimRequest Error-{0}:{1}", Common.ConvertIstanbulDateTime(DateTime.Now).ToString("s"), DateTime.UtcNow);
                    return new T();
                }
            }
        }


        private ConnectResponseModel MakeConnection()
        {
            try
            {
                //serialize request parameters as json
                var parameters = System.Text.Json.JsonSerializer.Serialize(_connectConfig);

                //send request
                var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{_connectConfig.Url}/IntegratorService/Connect?{parameters}");
                var response = RequestClient.Send(httpRequest);
                using var responseStream = response.Content.ReadAsStream();
                if (response.IsSuccessStatusCode)
                {
                    var rt = System.Text.Json.JsonSerializer.DeserializeAsync<ConnectResponseModel>(responseStream)
                        .ConfigureAwait(true).GetAwaiter().GetResult();

                    return rt;
                }
                else
                {
                    throw new Exception("Connection LogLogError.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw new HttpRequestException("Nebim connection Log Error.", e);
            }
        }

        private void DisposeConnection(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException(nameof(sessionId));

            try
            {
                var disposeRequest = new HttpRequestMessage(HttpMethod.Get,
                    $"{_connectConfig.Url}/(S({sessionId}))/IntegratorService/Disconnect");
                //dispose token request
                // var client = new RestClient(RequestUrl);
                var request = RequestClient.Send(disposeRequest);
                if (!request.IsSuccessStatusCode)
                {
                    throw new Exception("Connect dispose LogLogError");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw new HttpRequestException("Nebim token dispose LogError.", e);
            }
        }

    }


}