using System;
using System.Collections.Generic;

namespace FakeStore
{
  public class Cart
  {
    public List<CartItem> CartItems = new();
    public void Clear() { CartItems.Clear(); }
    public int Count => CartItems.Count;
    
  }
}