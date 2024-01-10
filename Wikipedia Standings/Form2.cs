using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wikipedia_Standings
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			string sparqlQuery = @"
            SELECT ?item ?itemLabel
            WHERE {
                ?item wdt:P7405 ?value.
                SERVICE wikibase:label { bd:serviceParam wikibase:language ""[AUTO_LANGUAGE]"". }
            }";

			string wikidataApiUrl = "https://www.wikidata.org/w/api.php";
			string format = "json";

			using (HttpClient client = new HttpClient())
			{
				// Construct the URL with query parameters
				var uriBuilder = new UriBuilder(wikidataApiUrl);
				var query = $"?format={format}&query={Uri.EscapeDataString(sparqlQuery)}";
				uriBuilder.Query = query;

				// Make the GET request
				HttpResponseMessage response = await client.GetAsync(uriBuilder.ToString());

				if (response.IsSuccessStatusCode)
				{
					string responseContent = await response.Content.ReadAsStringAsync();

					// Process the JSON response
					// Here you can deserialize the JSON and extract the desired data
					richTextBox1.AppendText(responseContent);
				}
				else
				{
					Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
				}
			}
			}
	}
}
