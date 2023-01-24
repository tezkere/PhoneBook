using ContactApi.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();
using
var channel = connection.CreateModel();
channel.QueueDeclare("PhoneBook_Report", exclusive: false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"ReportCreate message received: {message}");

    // User hizmetinden kullanıcı bilgisini al
    var client = new HttpClient();
    var userResponse = client.GetAsync($"http://localhost:5171/api/contact/getAllContact").GetAwaiter().GetResult();
    var userJson = userResponse.Content.ReadAsStringAsync().Result;
    Console.WriteLine(userJson);
    

};
//read the message
channel.BasicConsume(queue: "PhoneBook_Report", autoAck: true, consumer: consumer);
Console.ReadKey();