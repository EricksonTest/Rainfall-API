using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Rainfall_API.StepDefinitions
{
    [Binding]
    public class RainfallMeasurementsSteps
    {
        private readonly HttpClient _client = new()
        { 
            BaseAddress = new Uri("https://environment.data.gov.uk/flood-monitoring/id/floods") 
        };

        private HttpResponseMessage _response;
        private string _responseContent;
        private string _stationId;

        [Given(@"I have a valid station identifier ""(.*)""")]
        public void GivenIHaveAValidStationIdentifier(string stationId) => _stationId = stationId;

        // Reusable method to make API requests
        private async Task MakeApiRequestAsync(string query) 
        {
            _response = await _client.GetAsync($"stations/{_stationId}/rainfall?{query}");
            _responseContent = await _response.Content.ReadAsStringAsync();
        }

        [When(@"I request rainfall measurements with a limit of ""(.*)""")]
        public async Task WhenIRequestRainfallMeasurementsWithALimitOf(string limit) => 
            await MakeApiRequestAsync($"limit={limit}");

        [When(@"I request rainfall measurements for the date ""(.*)""")]
        public async Task WhenIRequestRainfallMeasurementsForTheDate(string date) => 
            await MakeApiRequestAsync($"date={date}");

        [Then(@"I should receive no more than ""(.*)"" measurements")]
        public void ThenIShouldReceiveNoMoreThanMeasurements(string expectedLimit)
        {
            Assert.IsTrue(_response.IsSuccessStatusCode, "API call failed.");
            var measurements = JArray.Parse(_responseContent);
            Assert.LessOrEqual(measurements.Count, int.Parse(expectedLimit), 
                $"Expected ≤ {expectedLimit} measurements, but got {measurements.Count}.");
        }

        [Then(@"all returned measurements should have the date ""(.*)""")]
        public void ThenAllReturnedMeasurementsShouldHaveTheDate(string expectedDate)
        {
            Assert.IsTrue(_response.IsSuccessStatusCode, "API call failed.");
            var measurements = JArray.Parse(_responseContent);

            foreach (var measurement in measurements)
            {
                Assert.AreEqual(expectedDate, measurement["date"]?.ToString(), 
                    "Measurement date mismatch.");
            }
        }
    }
}
