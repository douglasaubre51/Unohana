using CsvHelper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;
using System.Globalization;
using Unohana.Api.Models.SeedModels;
using Unohana.Api.Models.ServiceSettings;

namespace Unohana.Api.Data
{
    public static class SeedStudentInfo
    {
        public static void SeedCSVData(IApplicationBuilder builder)
        {
            using StreamReader streamReader = new(@"Data\studentInfo.csv");
            using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<StudentCSVModel>();
            Debug.WriteLine("loaded csv records!");

            try
            {
                // get ioptions service
                using var scope = builder.ApplicationServices.CreateScope();
                var options = scope.ServiceProvider.GetService<IOptions<MongoDbSettings>>();

                // get studentinfo collection
                Debug.WriteLine("connecting to neliel cluster!");
                MongoClient client = new MongoClient(options?.Value.ConnectionURI);
                var database = client.GetDatabase(options?.Value.DatabaseName);
                var collection = database.GetCollection<StudentCSVModel>(options?.Value.StudentInfoCollection);

                // delete all rows
                Debug.WriteLine("deleting existing database records!");
                var filter = Builders<StudentCSVModel>.Filter.Empty;
                collection.DeleteMany(filter);

                // insert all records
                Debug.WriteLine("inserting new records!");
                collection.InsertMany(records);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"seed student info error: {ex}");
            }
        }
    }
}
