using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StajUygulama.Forms;
using System.Windows.Forms;

namespace StajUygulama.MQTT
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
                //e.ApplicationMessage.Topic 
                // Console.WriteLine("Received application message.");
                // lblMessage.Invoke(new Action(() =>
                // {
                //     lblMessage.Text = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                // }));
                var strArr = e.ApplicationMessage.Topic.Split('/');
                var id = strArr[strArr.Length-1];

                formMain.frmWatch?.updateDeviceValue(id, Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                Console.WriteLine("topic: " + e.ApplicationMessage.Topic);
                Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

                return Task.CompletedTask;
            };
        }

        public void subscribe(string topic)
        {
            var mqttSubscribeOptions = new MqttTopicFilterBuilder()
               .WithTopic(topic)
               .WithAtLeastOnceQoS()
               .Build();

            mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public async Task initiliaze()
        {
            await connect_client();

            Handle_Received_Application_Message();
        }

        public async Task Publish_Application_Message(string msg, string topic)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(1, 10);
            msg = sayi.ToString();

            var mqttFactory = new MqttFactory();
            
            var applicationMessage = new MqttApplicationMessageBuilder()
                 .WithTopic(topic)
                 .WithPayload(msg)
                 .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                 .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            Console.WriteLine("MQTT application message is published.");
            

                //Random rnd2 = new Random();
                //int sayi2 = rnd2.Next(0, 2);
                //msg = sayi2.ToString();
               


        }
    }
}
