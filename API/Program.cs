using Newtonsoft.Json;

var client = new HttpClient();

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&limit=10"),
};

using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();

    var body = await response.Content.ReadAsStringAsync();

    dynamic data = JsonConvert.DeserializeObject(body);

    foreach (var item in data.results)
    {
        if (item.patient.drug[0].openfda != null)
        {
            Console.Write("Name: ");
            if (item.patient.drug[0].openfda.generic_name.HasValues)
                Console.WriteLine("" + item.patient.drug[0].openfda.generic_name[0]);

            Console.Write("Brand name: ");
            if (item.patient.drug[0].openfda.brand_name != null)
                Console.WriteLine("" + item.patient.drug[0].openfda.brand_name[0]);

            Console.Write("Indication: ");
            if (item.patient.drug[0].drugindication != null)
                Console.WriteLine("" + item.patient.drug[0].drugindication);
        }
        Console.WriteLine();
    }    
}
