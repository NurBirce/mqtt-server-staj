using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MqttServerStaj.Forms;
using System.Windows.Forms;

namespace MqttServerStaj.MQTT
{
    class Mqtt
    {
        IMqttClient mqttClient;
        FormMain formMain;
        public Mqtt(FormMain fm)
        {
            formMain = fm;
        }

        public async Task connect_client()
        {
            var mqttFactory = new MqttFactory();

            mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("test.mosquitto.org").Build();

            var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            Console.WriteLine("The MQTT client is connected.");

            Console.WriteLine(response);
        }

        public async Task disconnect()
        {
            var mqttFactory = new MqttFactory();

            var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

            await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
        }

        public void Handle_Received_Application_Message()
        {
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                try
                {
                    var topic = e.ApplicationMessage.Topic.Split('/');
                    var devicType = topic[2];
                    var id = topic[topic.Length - 1];

                    if (devicType == "dig")
                    {
                        formMain.frmWatch?.updateOnlyDigitalDevice(id, Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                    }
                    //else
                    //{
                    //    formMain.frmWatch?.updateAnalogDevice(id, Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                    //}
                    formMain.frmWatch?.sendDigitalsState();
                    Console.WriteLine("topic: " + e.ApplicationMessage.Topic);
                    Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception in RECIEVE: "+ex.ToString());
                }

                return Task.CompletedTask;
            };
        }

        public void subscribe(string topic)
        {
            var mqttSubscribeOptions = new MqttTopicFilterBuilder()
               .WithTopic("karatal2023fatmaproje/c/"+topic) // c: client does publish
               .WithAtLeastOnceQoS()
               .Build();

            mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }
        public void subscribeDigital(string topic)
        {
            subscribe("dig/" + topic);
        }
        public void subscribeAnalog(string topic)
        {
            subscribe("ana/" + topic);
        }

        public async Task initiliaze()
        {
            await connect_client();

            Handle_Received_Application_Message();
        }

        public async Task Publish(string msg, string topic)
        {
            var mqttFactory = new MqttFactory();
            
            var applicationMessage = new MqttApplicationMessageBuilder()
                 .WithTopic("karatal2023fatmaproje/s/" + topic)
                 .WithPayload(msg)
                 .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                 .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            Console.WriteLine("MQTT application message is published.");
        }
        public async Task PublishAnalog(string msg, string topic)
        {
            Publish(msg, "ana/" + topic);
        }
        public async Task PublishDigital(string msg, string topic)
        {
            Publish(msg, "dig/"+topic);
        }
    }
}
