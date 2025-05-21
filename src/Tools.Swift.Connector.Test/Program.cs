using Tools.Swift.Connector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace Tools.Swift.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            ISwiftClient swiftclient = serviceProvider.GetService<ISwiftClient>();

            var containers = swiftclient.GetAllContainersAsync().GetAwaiter().GetResult();

            foreach (var container in containers)
            {
                Console.WriteLine($"--- {container.Name} - {container.LastModified.ToShortDateString()} ---");

                var files = swiftclient.GetFilesByContainerAsync(container.Name).GetAwaiter().GetResult();

                foreach (var file in files)
                {
                    Console.WriteLine($"+ {file.Name} - {string.Format("{0:N}", file.Bytes / 1000)}Ko - {file.LastModified}");
                }

                if (container.Name == "test")
                {
                    string filename = "test.txt";

                    if (files.Any(file => file.Name == filename))
                    {
                        if (files.First(file => file.Name == filename).LastModified.Date < DateTime.UtcNow.Date)
                        {
                            byte[] fileByteArray = File.ReadAllBytes(filename);
                            bool success = swiftclient.CreateOrReplaceFileOnContainerAsync(container.Name, filename, fileByteArray)
                                                      .GetAwaiter()
                                                      .GetResult();
                            if (success)
                                Console.WriteLine($"{filename} Modified");
                        }
                    }
                    else
                    {
                        byte[] fileByteArray = File.ReadAllBytes(filename);
                        bool success = swiftclient.CreateOrReplaceFileOnContainerAsync(container.Name, filename, fileByteArray)
                                                  .GetAwaiter()
                                                  .GetResult();
                        if (success)
                            Console.WriteLine($"{filename} Created");
                    }
                }
            }
        }
    }
}
