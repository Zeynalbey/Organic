namespace Organic.Areas.Client.ViewModels.Product
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImgUrl { get; set; }
        //public List<CategoryListViewModel> Categories { get; set; }
        public string Category { get; set; }
        public List<DiscountPercentViewModel> DiscountPercents { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public ListItemViewModel(int id, string name, string info, decimal price,
            string imgUrl, string category,
            List<DiscountPercentViewModel> discountPercents, List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Info = info;
            Price = price;
            ImgUrl = imgUrl;
            Category = category;
            DiscountPercents = discountPercents;
            Tags = tags;
        }

        public ListItemViewModel() { }
        public class DiscountPercentViewModel
        {
            public DiscountPercentViewModel(decimal percent)
            {
                Percent = percent;
            }

            public decimal Percent { get; set; }
        }
        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
    }
}
