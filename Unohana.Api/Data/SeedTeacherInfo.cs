using CsvHelper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Globalization;
using Unohana.Api.Models.SeedModels;
using Unohana.Api.Models.ServiceSettings;

namespace Unohana.Api.Data
{
    public static class SeedTeacherInfo
    {
        public static void SeedCSVData(IApplicationBuilder app)
        {
            // fetch csv
            using StreamReader streamReader = new StreamReader(@"Data\teacherInfo.csv");
            using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<TeacherCSVModel>();

            // fetch ioptions
            using var scope = app.ApplicationServices.CreateScope();
            var options = scope.ServiceProvider.GetService<IOptions<MongoDbSettings>>();

            // fetch mongodb client 
            var client = new MongoClient(options?.Value.ConnectionURI);
            var database = client.GetDatabase(options?.Value.DatabaseName);
            var collection = database.GetCollection<TeacherCSVModel>(options?.Value.TeacherInfoCollection);

            // delete existing documents
            var filter = Builders<TeacherCSVModel>.Filter.Empty;
            collection.DeleteMany(filter);

            // insert new records
            collection.InsertMany(records);
        }
    }
}
