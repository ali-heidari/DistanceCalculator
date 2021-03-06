
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Core.Models;
using Core.NServiceBus;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebAPI.RabbitMQ
{
    public class RabbitManager : IRabbitManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }
        private string Serialize(Object obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(obj.GetType());
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, obj);
                    xml = sww.ToString(); // Your XML
                }
            }
            return xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\"?>");
        }
        public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class
        {
            if (message == null)
                return;

            var channel = _objectPool.Get();

            try
            {
                channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                // var SerializeObject = new System.Xml.Serialization.XmlSerializer(typeof(GeoPoints));
                // var sendBytes = Encoding.UTF8.GetBytes(Serialize(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.MessageId = Guid.NewGuid().ToString();
                var typeName = typeof(T).FullName;
                properties.Headers = new Dictionary<string, object> { { "NServiceBus.EnclosedMessageTypes", typeName } };
                var correlationId = Guid.NewGuid().ToString();
                properties.CorrelationId = correlationId;
                string replyQueueName = "RabbitMQ";//channel.QueueDeclare().QueueName;
                properties.ReplyTo = replyQueueName;

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var response = Encoding.UTF8.GetString(body);
                    var msg = JsonConvert.DeserializeObject<GeoPoints>(response.Substring(1));
                    if (msg != null)
                    {
                        Core.Data.DataProvider db = Core.Data.DataProvider.DataProviderFactory();
                        // Insert into database
                        bool res = db.Insert(new Core.Data.GeoData()
                        {
                            Distance = msg.Distance,
                            StartingLat = msg.StartingLat,
                            StartingLng = msg.StartingLng,
                            EndingLat = msg.EndingLat,
                            EndingLng = msg.EndingLng,
                            UserGUID = msg.UserGUID
                        });

                    }
                    ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(
                    consumer: consumer,
                    queue: replyQueueName,
                    autoAck: false);

                channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}