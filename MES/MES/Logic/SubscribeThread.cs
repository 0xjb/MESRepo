using System.Collections.Generic;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;

namespace MES.Logic
{
    class SubscribeThread
    {
        public void Subscript()
        {
            Session sessionSubscript = new Session();
            sessionSubscript.Connect("opc.tcp://127.0.0.1:4840", SecuritySelection.None);
            Subscription subscription = new Subscription(sessionSubscript);

            NodeId nodeId = new NodeId("::Program:Cube.Admin.ProdProcessedCount", 6);
            MonitoredItem monitoredItem = new DataMonitoredItem(nodeId);
            List<MonitoredItem> listOfMonitoredItems = new List<MonitoredItem>();
            listOfMonitoredItems.Add(monitoredItem);

            subscription.CreateMonitoredItems(listOfMonitoredItems);

            subscription.PublishingEnabled = true;
            subscription.PublishingInterval = 500;


        }
    }
}
