﻿using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace MISP
{
    public class Cart
    {
        public List<CartLine> Lines = new List<CartLine>();
        public void AddItem(Product product, int quantity)
        {
            CartLine line = Lines
               .Where(p => p.Product.Id == product.Id)
               .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            Lines.RemoveAll(l => l.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return Lines.Sum(e => Convert.ToDecimal(e.Product.Price) * e.Quantity);
        }
        public void Clear()
        {
            Lines.Clear();
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public float GetCost()
        {
            return Product.Price * Quantity;
        }
    }
}