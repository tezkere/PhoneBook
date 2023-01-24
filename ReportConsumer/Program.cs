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

    //// User hizmetinden kullanıcı bilgisini al
    //var client = new HttpClient();
    //var userResponse = await client.GetAsync($"http://localhost:5171/api/contact/GetAll");
    //var userJson = await userResponse.Content.ReadAsStringAsync();
    //var user = JsonConvert.DeserializeObject<User>(userJson);

    //// Kullanıcıya ait siparişleri al
    //var orders = _context.Orders.Where(o => o.UserId == userId).ToList();

    //// Kullanıcı bilgilerini sipariş bilgilerine ekle
    //orders.ForEach(o => o.User = user);

    //return Ok(orders);

};
//read the message
channel.BasicConsume(queue: "PhoneBook_Report", autoAck: true, consumer: consumer);
Console.ReadKey();