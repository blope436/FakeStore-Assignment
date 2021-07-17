using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FakeStore
{
  //InventoryContext will define connection and struture of database
  public class InventoryContext : DbContext
  {
    public DbSet<Inventory> Inventory1 {get; set;}
    public DbSet<Orders> Order {get; set;}
    //create the OnConfiguring to crate a databse in the current directory
    protected override void OnConfiguring(DbContextOptionsBuilder opt) => opt.UseSqlite(@"Data Source=inventory.sqlite");
  }
  public class Inventory : IEnumerable
  {
    //Create variable that will correspond to the items in the program
    //These variables will be used to create the database
    public int InventoryID {get; set;}

    public string inventoryNumber {get; set;}

    public string inventoryName {get; set;}

    public double inventoryPrice {get; set;}

    public int inventoryQuantity {get;set;}

    public List<Orders> orders = new();

    public List<Item> Items = new();
    public void Clear() => Items.Clear();
    public void Add(Item item) => Items.Add(item);
    public int Count() => Items.Count;

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

    
  }
  public class Orders
  {
    //Create variable that will correspond to the items in the program
    //These variables will be used to create the database- This is for the Orders table- Will be linked to the Inventory Table
    public int OrdersId {get; set;}
    public string productName {get; set;}
    public double productPrice {get;set;}

    public int productQuantity {get;set;}

    public double total {get; set;}

    public double subTotal {get; set;}

    public double taxes {get; set;}

    public double allTotal {get; set;}

    //reference an inventory item-one to many connection
    public int InventoryID {get; set;}

    public Inventory Inventory {get; set;}

  }
}