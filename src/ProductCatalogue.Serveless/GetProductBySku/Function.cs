using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductCatalogue.Application.Queries;
using ProductCatalogue.Application.Setup;
using ProductCatalogue.Infrastructure;
using ProductCatalogue.Infrastructure.LambdaLogger;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetProductBySku
{
    public class Function
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private readonly Guid _tenantId;
        private readonly Lazy<IMediator> _mediatr;

        // example payload { "tenantId": "743872ea-7e68-421b-9f98-e09f35d76117", "sku": "HOU/IN/82" }
        public Function()
        {
            _serviceCollection = new ServiceCollection()
                .AddApplicationServices()
                .AddPersistenceServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            _mediatr = new Lazy<IMediator>(() => _serviceProvider.GetRequiredService<IMediator>());

            // JUST FOR TESTING, forces the tenant ID to be a known one so the user doesn't have to remember it
            this._tenantId = Guid.Parse("743872ea-7e68-421b-9f98-e09f35d76117");
        }


        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger>();
            logger.SetLoggerContext(context.Logger);
            logger.LogInfo($"Fetching product by SKU for tenant {_tenantId}");

            try
            {
                // just for testing, the tenant id would be a guid in a real app
                Guard.Against.Null(request, nameof(request));
                var query = JsonConvert.DeserializeObject<GetProductBySkuQuery>(request.Body);

                // fire command (tenantId should come from the data passed but this is for testing)
                query.TenantId = _tenantId;

                logger.LogInfo($"Fetching product by SKU {query.Sku} for tenant {query.TenantId}");
                var queryResult = await _mediatr.Value.Send(new GetProductBySkuQuery(query.TenantId, query.Sku));

                // return result
                if (queryResult == null)
                {
                    logger.LogInfo($"Product with SKU {query.Sku} not found for tenant {query.TenantId}");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Body = $"Product with SKU {query.Sku} not found for tenant {query.TenantId}"
                    };
                }

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(queryResult)
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching product by SKU for tenant {_tenantId} - {ex.Message}");
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = $"Error fetching product by SKU for tenant {_tenantId} - {ex.Message}"
                };
            }
        }
    }
}
