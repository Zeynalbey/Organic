﻿namespace Organic.Areas.Client.ViewModels.OrderProducts
{
    public class OrdersProductsViewModel
    {
        public List<ItemViewModel>? Products { get; set; }
        public SummaryViewModel? Summary { get; set; }

        public class SummaryViewModel
        {
            public decimal Total { get; set; }
        }

        public class ItemViewModel
        {
            public string? Name { get; set; }
            public string? Image { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }
    }
}
