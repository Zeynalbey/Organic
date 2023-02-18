namespace Organic.Areas.Admin.ViewModels.Product.Tag
{
    public class TagViewModel
    {
        public TagViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
