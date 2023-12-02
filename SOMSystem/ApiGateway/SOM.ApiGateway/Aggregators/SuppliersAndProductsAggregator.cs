using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.Configuration;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using SOM.ApiGateway.Common.Dto;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace SOM.ApiGateway.Aggregators
{
    public class SuppliersAndProductsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            List<SupplierDto>? suppliers = new List<SupplierDto>();
            List<ProductDto>? products = new List<ProductDto>();

            foreach (var response in responses)
            {
                string downStreamRouteKey = ((DownstreamRoute)response.Items["DownstreamRoute"]).Key;
                DownstreamResponse downstreamResponse = (DownstreamResponse)response.Items["DownstreamResponse"];
                var downstreamResponseContent = await downstreamResponse.Content.ReadAsStringAsync();

                if (downStreamRouteKey == "suppliers")
                {
                    suppliers = JsonConvert.DeserializeObject<List<SupplierDto>>(downstreamResponseContent);
                }

                if (downStreamRouteKey == "products")
                {
                    products = JsonConvert.DeserializeObject<List<ProductDto>>(downstreamResponseContent);
                }

            }

            return MapSupplierToProduct(suppliers, products);

            // This also a another method.
            //var one = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            //var two = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
            //var contentBuilder = new StringBuilder();
            //contentBuilder.Append(one);
            //contentBuilder.Append(two);
            //var stringContent = new StringContent(contentBuilder.ToString())
            //{
            //    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            //};
            //return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<Header>(), "OK");
        }

        private DownstreamResponse MapSupplierToProduct(List<SupplierDto> suppliers,List<ProductDto> products)
        {
            JObject result = new JObject();
            
            foreach (var product in products)
            {
                var selectsuppliersWithProducts = JsonConvert.SerializeObject(suppliers.Select(n => new { n.Id, n.Name, n.Address, product.Description }));
                var selectsuppliersWithProductsString = JsonConvert.DeserializeObject<JArray>(selectsuppliersWithProducts);
                result.Add(new JProperty(product.Name??"test", selectsuppliersWithProductsString));
            }

            var ProductsString = JsonConvert.SerializeObject(result);

            var stringContent = new StringContent(ProductsString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

        }
    }
}
