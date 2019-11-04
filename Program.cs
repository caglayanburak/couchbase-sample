using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;

namespace CouchSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("http://127.0.0.1:8091") }
            });

            var authenticator = new PasswordAuthenticator("Administrator", "123456");
            cluster.Authenticate(authenticator);

            using (var bucket = cluster.OpenBucket("InventoryItem"))
            {
                var document = new Document<List<InventoryItem>>
                {
                    Id = "InventoryItem",
                    Content = new List<InventoryItem>()
                    {
                        new InventoryItem
                        {
                            Id = 1,Code = "Test",Description = "Test"
                        }
                    }
                };

                var upsert = bucket.Upsert(document);
                if (upsert.Success)
                {
                    System.Console.WriteLine(upsert.Success);
                }
            }

            Console.ReadLine();
        }
    }

    public class InventoryItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
