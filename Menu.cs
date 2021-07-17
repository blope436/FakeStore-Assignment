using System;

namespace FakeStore
{
  public class Menu
  {
    public void Print()
    {
    
      Console.WriteLine("WELCOME TO THE FAKESHOP\n");
      Console.WriteLine("What would you like to do? \n");
      Console.WriteLine("Type 'add' to add an item.");
      Console.WriteLine("Type 'show' to show your list.");
      Console.WriteLine("Type 'clear' to delete all items from list.");
      Console.WriteLine("Type 'done' to purchase.\n");
      Console.WriteLine("Type 'quit' to quit.\n\n");
    }
  }
}