using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductCatalogue.Application.Queries;
using ProductCatalogue.Application.Setup;
using ProductCatalogue.Infrastructure;
using ProductCatalogue.Infrastructure.LambdaLogger;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetProducts
{
    public class Function
    {

        private readonly IServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private Guid _tenantId;
        private readonly Lazy<IMediator> _mediatr;

        public Function()
        {
            _serviceCollection = new ServiceCollection()
                .AddApplicationServices()
                .AddPersistenceServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            _mediatr = new Lazy<IMediator>(() => _serviceProvider.GetRequiredService<IMediator>());

            // JUST FOR TESTING, forces the tenant ID to be a known one so the user doesn't have to remember it
            _tenantId = Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117");
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            // performs a query for all products for a tenant (tenant id would be from a parameter in real life).
            var logger = _serviceProvider.GetRequiredService<ILogger>();
            logger.SetLoggerContext(context.Logger);
            logger.LogInfo($"Fetching all products for tenant {_tenantId}");

            try
            {
                var query = new GetProductsQuery(_tenantId);
                var queryResponse = await _mediatr.Value.Send(query);

                logger.LogInfo($"Returning {queryResponse.Count()} products");

                // return result
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(queryResponse),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching products: {ex.Message}");
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = JsonConvert.SerializeObject(ex.Message),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
        }
    }
}
