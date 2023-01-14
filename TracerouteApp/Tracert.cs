using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace TracerouteApp;

public class Tracert
{
    public static IEnumerable<Node> Run(string ip, int maxHops, int timeout)
    {
        if (!IPAddress.TryParse(ip, out var address))
        {
            address = Dns.GetHostAddresses(ip).FirstOrDefault();
            if (address == null)
            {
                throw new ArgumentException("Не удается разрешить системное имя узла");
            }
        }

        if (maxHops < 1)
        {
            throw new ArgumentException("Максимальное число хопов должно быть положительным");
        }

        if (timeout < 1)
        {
            throw new ArgumentException("Время ожидания должно быть положительным");
        }

        Console.WriteLine($"Трассировка маршрута {ip} [{address}]\nс максимальным числом прыжков {maxHops}:\n");

        using var pinger = new Ping();
        for (var ttl = 1; ttl <= maxHops; ttl++)
        {
            var pingOptions = new PingOptions(ttl, true);
            var pingReplyTime = new Stopwatch();

            pingReplyTime.Start();
            var reply = pinger.Send(address, timeout, new byte[32], pingOptions);
            pingReplyTime.Stop();

            var hopNumber = ttl;
            var responseTime = pingReplyTime.ElapsedMilliseconds;
            var hostname = GetHostname(reply.Address);
            var nodeAddress = reply.Address == null ? " -- " : reply.Address.ToString();

            yield return new Node(hopNumber, responseTime, hostname, nodeAddress, reply.Status);
        }
        
    }

    private static string GetHostname(IPAddress address)
    {
        try
        {
            return address == null ? "" : Dns.GetHostEntry(address).HostName;
        }
        catch (SocketException)
        {
            return "";
        }
    }
}
