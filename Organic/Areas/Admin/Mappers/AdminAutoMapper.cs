//using AutoMapper;
//using Organic.Areas.Admin.ViewModels.Blog;
//using Organic.Contracts.File;
//using Organic.Database.Models;
//using Organic.Services.Abstracts;

//namespace Organic.Areas.Admin.Mappers
//{
//    public class AdminAutoMapper : Profile
//    {
//        private readonly IFileService _fileService;

//        public AdminAutoMapper()
//        {
//            CreateMap<Blog, BlogListViewModel>()

//                .ForMember(dest => dest.From, opt => opt.MapFrom(src => $"{src.From!.FirstName} {src.From.LastName}"))
//                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BlogAndCategories!.Select(bac => bac.BlogCategory)
//                    .Select(bc => new BlogCategoryViewModel(bc.Id, bc.Name!)).ToList()))
//                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.LikeCount))
//                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments!.Select(c => new BlogCommentListViewModel(c.Id, c.CommentDate, c.Text,
//                    $"{src.From.FirstName} {src.From.LastName}", c.Blog!.Title)).ToList()))
//                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => _fileService.GetFileUrl(src.ImageNameInSystem, UploadDirectory.Blog)));
//        }
//    }
//}
