namespace Mongemini.Bus.RabbitMq.Options
{
    public class RabbitMqOption
    {
        public const string RabbitMq = "RabbitMq";

        public string Host { get; set; }

        public int? Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
