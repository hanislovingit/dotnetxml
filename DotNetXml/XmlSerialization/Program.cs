﻿using System;
using System.IO;
using System.Xml.Serialization;
using XmlSerialization.models;

namespace XmlSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======= this is a console app to test xml serialization =======");

            var program = new Program();
            program.CreatePO("PurchaseOrder.xml");

            Console.WriteLine("======= Purchase order has been successfully serialized to xml =======");

            Console.ReadLine();
        }

        private void CreatePO(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));
            TextWriter writer = new StreamWriter(filename);
            PurchaseOrder po = new PurchaseOrder();

            // Creates an address to ship and bill to.
            Address billAddress = new Address();
            billAddress.Name = "Teresa Atkinson";
            billAddress.Line1 = "1 Main St.";
            billAddress.City = "AnyTown";
            billAddress.State = "WA";
            billAddress.Zip = "00000";
            // Sets ShipTo and BillTo to the same addressee.
            po.ShipTo = billAddress;
            po.OrderDate = DateTime.Now.ToLongDateString();

            // Creates an OrderedItem.
            OrderedItem i1 = new OrderedItem();
            i1.ItemName = "Widget S";
            i1.Description = "Small widget";
            i1.UnitPrice = (decimal)5.23;
            i1.Quantity = 3;
            i1.Calculate();

            // Inserts the item into the array.
            OrderedItem[] items = { i1 };
            po.OrderedItems = items;
            // Calculate the total cost.
            decimal subTotal = new decimal();
            foreach (OrderedItem oi in items)
            {
                subTotal += oi.LineTotal;
            }
            po.SubTotal = subTotal;
            po.ShipCost = (decimal)12.51;
            po.TotalCost = po.SubTotal + po.ShipCost;

            // Serializes the purchase order, and closes the TextWriter.
            serializer.Serialize(writer, po);
            writer.Close();
        }
    }
}
