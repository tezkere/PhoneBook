using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportConsumer;
using System.Text;
using Newtonsoft.Json;

var factory = new ConnectionFactory
{
    HostName = "localhost",
};

var connection = factory.CreateConnection();
using
var channel = connection.CreateModel();
channel.QueueDeclare("PhoneBook_Report", exclusive: false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"ReportCreate message received: {message}");
        
    using 
    var contact = new HttpClient();
    var contactResponse = contact.GetAsync($"http://localhost:5171/api/contact/getReport").GetAwaiter().GetResult();
    var reportInfoJson = contactResponse.Content.ReadAsStringAsync().Result;

    var reportInfoList = JsonConvert.DeserializeObject<List<ReportInfo>>(reportInfoJson);

    var msg = JsonConvert.DeserializeObject<string>(message);

    Guid.TryParse(msg, out var reportId);

    foreach (var info in reportInfoList) {
        info.ReportId = reportId;
    }

    var sendingContent = JsonConvert.SerializeObject(reportInfoList);

    Console.WriteLine($"ReportInfo Created by ContractApi: {reportInfoJson}");

    using
    var report = new HttpClient(); 
    var createReportResponse = report.PostAsync($"http://localhost:5196/api/Report/createReportDetail", new StringContent(sendingContent,Encoding.UTF8,"application/json")).GetAwaiter().GetResult();
    var createdSuccessJson = createReportResponse.Content.ReadAsStringAsync().Result;

    Console.WriteLine($"ReportInfo Created in ReportApi: {createdSuccessJson}");
};
//read the message
channel.BasicConsume(queue: "PhoneBook_Report", autoAck: true, consumer: consumer);
Console.ReadKey();