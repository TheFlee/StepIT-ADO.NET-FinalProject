using Microsoft.Extensions.Configuration;
using StepIT_ADO.NET_FinalProjectl;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var config = builder.Build();
string connectionString = config.GetConnectionString("MyJCS")!;

using var db = new QuizContext(connectionString);

