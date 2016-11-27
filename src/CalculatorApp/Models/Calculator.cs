namespace CalculatorApp.Models
{
    using System.Threading.Tasks;
    using RestSharp.Portable;
    using RestSharp.Portable.HttpClient;

    public class Calculator : ICalculator
    {
        private const string BaseUrl = "http://localhost:5000";

        public Calculator()
        {

        }

        async Task<CalculatorResult> ICalculator.Evaluate(string expression)
        {
            var request = new RestRequest("api/calculations", Method.POST);
            request.AddBody(new { expression });

            var client = new RestClient(BaseUrl);

            try
            {
                var response = await client.Execute<ResponseData>(request);

                if (response.IsSuccess)
                    return new CalculatorResult(expression, response.Data.Result);
            } 
            catch { }

            return null;
        }
    }

    public class ResponseData
    {
        public string Expression { get; set; }
        public decimal Result { get; set; }
    }
}