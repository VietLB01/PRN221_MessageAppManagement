using ContractLibrary.Models;
using Server;
using System.ServiceModel;
using System.ServiceProcess;

internal class Program : ServiceBase
{
    private static void Main(string[] args)
    {
        var urls = new Uri[1];
        string address = "http://localhost:8080/MessageService";
        IMessageService service = new MessageServices();

    }
}