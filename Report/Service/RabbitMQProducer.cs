namespace ReportApi.Service
{
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using System.Text;
    public interface IRabbitMQProducer
    {
        public void SendReportCreateMessage<T>(T message);
    }

    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendReportCreateMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("PhoneBook_Report", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "PhoneBook_Report", body: body);
        }
    }
}
