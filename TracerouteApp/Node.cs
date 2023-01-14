using System.Net.NetworkInformation;

namespace TracerouteApp;

/// <summary>
/// Class for hop node
/// </summary>
public class Node
{
    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="hopNumber">Hop count number</param>
    /// <param name="responseTime">Response time in ms</param>
    /// <param name="hostname">Node name</param>
    /// <param name="address">Node ip address</param>
    /// <param name="responseStatus">Response status</param>
    public Node(int hopNumber, long responseTime, string hostname, string address, IPStatus responseStatus)
    {
        HopNumber = hopNumber;
        ResponseTime = responseTime;
        Hostname = hostname;
        Address = address;
        ResponseStatus = responseStatus;
    }

    /// <summary>
    /// Hop count number
    /// </summary>
    public int HopNumber { get; private set; }

    /// <summary>
    /// Response time in ms
    /// </summary>
    public long ResponseTime { get; private set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string Hostname { get; private set; }

    /// <summary>
    /// Node ip address
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    /// Response status
    /// </summary>
    public IPStatus ResponseStatus { get; private set; }

    public override string ToString()
    {
        var hostname = string.IsNullOrEmpty(Hostname) ? Address : $"{Hostname} [{Address}]";

        var line = ResponseStatus == IPStatus.TimedOut
            ? string.Format("{0,2}  {1,7}  {2,-10}", $"{HopNumber}", "*     ", "Превышен интервал ожидания для запроса.")
            : string.Format("{0,2}  {1,7}  {2,-10}", $"{HopNumber}", $"{ResponseTime} ms  ", hostname);

        return line;
    }
}
