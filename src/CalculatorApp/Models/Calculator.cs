namespace CalculatorApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RestSharp.Portable;
    using RestSharp;
    using RestSharp.Portable.HttpClient;

    public class Calculator : ICalculator
    {
        private const string BaseUrl = "http://localhost:5000";

        public Calculator()
        {

        }

        async Task<decimal> ICalculator.Evaluate(string expression)
        {
            var request = new RestRequest(string.Format("api/calculations/{0}", expression), Method.GET);
            var client = new RestClient(BaseUrl);
            var response = await client.Execute<ResponseData>(request);

            if (response.IsSuccess)
                return response.Data.Result;

            return Convert.ToDecimal(0);
        }
    }

    public class ResponseData
    {
        public string Expression { get; set; }
        public decimal Result { get; set; }
    }
}