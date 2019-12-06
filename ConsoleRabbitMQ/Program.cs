using System;
using RabbitMQ.Client;
using System.Text;

namespace ConsoleRabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================================");
            Console.WriteLine("RabbitMQ envio mensagem!");
            Console.WriteLine("================================================");

            var connectionFactory = new ConnectionFactory()
            {
                UserName = "teste",
                Password = "teste",
                HostName = "10.20.34.31",
                Port = 5672
            };

            var connection = connectionFactory.CreateConnection();

            string msg = "";
            while (!msg.Equals("q"))
            {
                msg = null;
                Console.Write("Informe nova mensagem (ou 'q' para sair).\n => ");
                msg = Console.ReadLine();
                if ( !msg.Equals("q") )
                {
                    Console.Write("\nEnviando mensagem: '" + msg + "'...");
                    using (var channel = connection.CreateModel())
                    {
                        byte[] body = Encoding.UTF8.GetBytes(msg);
                        channel.BasicPublish(exchange: "",
                            routingKey: "Cbo",
                            basicProperties: null,
                            body: body
                            );
                    }
                    Console.WriteLine("\nMensagem enviada! ");
                }
            } ;


            Console.WriteLine("================================================");
            Console.WriteLine("fim.");
            Console.WriteLine("================================================");
        }
    }
}
