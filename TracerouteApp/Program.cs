using TracerouteApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;
while (true)
{
    Console.Write("Введите IP-aдрес: ");
    var address = Console.ReadLine();

    try
    {
        foreach (var tracertEntry in Tracert.Run(address, 30, 10000))
        {
            Console.WriteLine(tracertEntry);
        }
        Console.WriteLine("Трассировка завершена.\n");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    Console.WriteLine();
}