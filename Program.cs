using System;
using FakeStore;
using System.Linq;
using System.Collections.Generic;

//These variables of type Item will be used to sent the information from here to the database inventory.sqlite
Item imac = new() { Name = "iMac Latest", Price = 1_200 };
Item macmini = new() { Name = "Mac Mini", Price = 700 };
Item mbair = new() { Name = "MacBook Air", Price = 1_000 };
Item mbpro = new() { Name = "MacBook Pro", Price = 1_300 };
//will be used to display the menu
Menu menu = new();
//inventory will be used to store the avove varibales in the database
Inventory inventory = new();
Cart cart = new();

inventory.Add(imac);
inventory.Add(macmini);
inventory.Add(mbair);
inventory.Add(mbpro);

//create a new variable db that will be used to send the products to the database
//On first run the databse will get filled witht he inventory items, this will be used for the orders table
using(var db = new InventoryContext())
{
    Console.WriteLine("Adding four new items in database");
    db.Add(new Inventory {inventoryNumber = imac.ID ,inventoryName = imac.Name,inventoryPrice = imac.Price, inventoryQuantity = 20});
    db.Add(new Inventory {inventoryNumber = macmini.ID ,inventoryName = macmini.Name,inventoryPrice = macmini.Price, inventoryQuantity = 40});
    db.Add(new Inventory {inventoryNumber = mbair.ID ,inventoryName = mbair.Name,inventoryPrice = mbair.Price, inventoryQuantity = 60});
    db.Add(new Inventory {inventoryNumber = mbpro.ID ,inventoryName = mbpro.Name,inventoryPrice = mbpro.Price, inventoryQuantity = 10});
    db.SaveChanges();
}

while (true)
{
  //Print the menu
  menu.Print();
 
  Console.Write("Option: ");
  //User input
  string response = Console.ReadLine();
  switch (response)
  {
    case "add":
      {
        Console.WriteLine("WELCOME TO THE FAKESHOP - Our Products\n");
      Console.WriteLine("      ID            NAME              PRICE");
     
      Console.WriteLine(String.Format("{0} {1,15} \t${2,10:N2}", imac.ID, imac.Name, imac.Price));
      Console.WriteLine(String.Format("{0} {1,15} \t${2,10:N2}", macmini.ID, macmini.Name, macmini.Price));
      Console.WriteLine(String.Format("{0} {1,15} \t${2,10:N2}", mbair.ID, mbair.Name, mbair.Price));
      Console.WriteLine(String.Format("{0} {1,15} \t${2,10:N2}", mbpro.ID, mbpro.Name, mbpro.Price));
     
        Console.Write("\nWhat item would you like to add? (Type ID) ");
        string productID = Console.ReadLine();

        Item product = inventory.Items.Find(x => x.ID == productID);
        
        
        Console.Write("\nHow Many? ");
        

        if (product is not null & Int16.TryParse(Console.ReadLine(), out short productQty))
        {
          //newitem of type Inventory will be used to add information in the database
          Inventory newitem = new();
          //items of type Item to add information in the databse
          //Item items = new();
          //cartItem will be used to send informaion into the database
          Cart cartItem = new();
          //ci will be used to hold the product and productQty
          CartItem ci = new();
          ci.Product = product;
          ci.Quantity = productQty;
          cart.CartItems.Add(ci);
          
        using(var db2 = new InventoryContext())
        {
            Console.WriteLine("\nAdding the orders to the order table\n");
            //When any of the ids are equall to a certain product they will get inserted into the database
            if(productID == imac.ID)
            {
            db2.Add(new Orders {productName = imac.Name, productPrice = imac.Price, productQuantity = ci.Quantity, total = imac.Price * ci.Quantity, subTotal = imac.Price * ci.Quantity, taxes = imac.Price * ci.Quantity * (8.25 / 100), allTotal = imac.Price * ci.Quantity + imac.Price * ci.Quantity * (8.25 / 100),InventoryID = 1});
            }
           
            if(productID == macmini.ID)
            {
            db2.Add(new Orders {productName = macmini.Name, productPrice = macmini.Price, productQuantity = ci.Quantity, total = macmini.Price * ci.Quantity, subTotal = macmini.Price * ci.Quantity, taxes =  macmini.Price * ci.Quantity * (8.25 / 100), allTotal = macmini.Price * ci.Quantity + macmini.Price * ci.Quantity * (8.25 / 100), InventoryID = 2});
            }
            
            if(productID == mbair.ID)
            {
            db2.Add(new Orders {productName = mbair.Name, productPrice = mbair.Price, productQuantity = ci.Quantity, total = mbair.Price * ci.Quantity, subTotal = mbair.Price * ci.Quantity, taxes = mbair.Price * ci.Quantity * (8.25 / 100), allTotal = mbair.Price * ci.Quantity + mbair.Price * ci.Quantity * (8.25 / 100), InventoryID = 3});
            }
           
            if(productID == mbpro.ID)
            {
            db2.Add(new Orders {productName = mbpro.Name, productPrice = mbpro.Price, productQuantity = ci.Quantity, total = mbpro.Price * ci.Quantity, subTotal = mbpro.Price * ci.Quantity, taxes = mbpro.Price * ci.Quantity * (8.25 / 100), allTotal = mbpro.Price * ci.Quantity + mbpro.Price * ci.Quantity * (8.25 / 100), InventoryID = 4});
            }
            db2.SaveChanges();
        }
        }
        else
        {
          Console.WriteLine("\nNo product with that ID or Incorrect Qty");
        }
        break;
        
      }
    case "show":
      { 
        //allTotal will be used to calculate all of the items entered by the user
        double allTotal = 0;
        //Print the information in the database
        using(var db3 = new InventoryContext())
        {
          List<Orders> orders = db3.Order.ToList<Orders>();
          //go thorugh all of the items in the order table and add the total of all items
          orders.ForEach(o => allTotal += o.allTotal);
          orders.ForEach(o => Console.WriteLine($"\nProduct Name: {o.productName}\n\nProduct Price: ${o.productPrice}\n\nProduct Quantity: {o.productQuantity}\n\nProduct Total: ${o.total}\n\nProduct SubTotal: ${o.subTotal}\n\nProduct Taxes: ${o.taxes}\n\nTotal: ${o.allTotal}"));

        }
        Console.WriteLine("\n\nTotal for all Items: ${0}",allTotal);
      
        break;
      }
    case "clear":
      {
        //Clear the order from the screen
        Console.Clear();
        break;
      }
    case "done":
      {
        //allTotal will be used to calculate the total of all items ordered
        double allTotal = 0;
        
        //Print all of the data from the database
        using(var db3 = new InventoryContext())
        {
          //All of the output will come from the items in the database
          List<Orders> orders = db3.Order.ToList<Orders>();
          orders.ForEach(o => allTotal += o.allTotal);       
          orders.ForEach(o => Console.WriteLine($"\nProduct Name:{o.productName}\n\nProduct Price: ${o.productPrice}\n\nProduct Quantity: {o.productQuantity}\n\nProduct Total: ${o.total}\n\nProduct SubTotal: ${o.subTotal}\n\nProduct Taxes: ${o.taxes}\n\nTotal: ${o.allTotal}"));
    
          
        }
        Console.WriteLine("\n\nTotal for all Items: ${0}",allTotal);
        
        Console.Write("\n\nPress any key to continue...");
        Console.ReadKey();
        //clear the orders
        Console.Clear();
        Environment.Exit(0);
        break;
      }
    case "quit":
      {
        Environment.Exit(0);
        break;
      }

      
      
  }
 
         
        }

