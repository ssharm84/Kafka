Step 1:
----------------------------------------------------------------------------------------------------------------------------------
cd C:/kafka
.\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties
.\bin\windows\kafka-server-start.bat .\config\server.properties
.\bin\windows\kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic TestTopic
.\bin\windows\kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic NewTopic
.\bin\windows\kafka-topics.bat --list --zookeeper localhost:2181
.\bin\windows\kafka-console-producer.bat --broker-list localhost:9092 --topic TestTopic
.\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic TestTopic --from-beginning

--------------------------------------------------------------------------------------------------------------
cd C:/kafka
.\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties
.\bin\windows\kafka-server-start.bat .\config\server.properties
cd /bin/windows:
kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic ss-Topic
kafka-topics.bat --list --zookeeper localhost:2181
C:\kafka\bin\windows>kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic ss-Topic --from-beginning

----------------------------------------------------------------------------------------------------------------------------------
Step 2:
1.dotnet new webapi -o kafkamicro
2.dotnet add package Confluent.kafka
3.appsettings.json:
    "producer": {
    "bootstrapservers": "localhost:9092"
  }
4.Inside Startup.cs:
    var producerConfig = new ProducerConfig();//using Confluent.kafka;
            Configuration.Bind("producer",producerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);
5.Models->Employee.cs
6.dotnet add package Newtonsoft.Json
7.Controller->ProducerController.cs
8.dotnet run
9.Postman-Post->http://localhost:5000/Producer/send?topic=ss-Topic
Body->raw{
        "Id": 2,
        "Name": "Shane"
}  
