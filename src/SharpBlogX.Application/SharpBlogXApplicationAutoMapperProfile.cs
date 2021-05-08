using AutoMapper;
using SharpBlogX.Domain.Blog;
using SharpBlogX.Domain.Hots;
using SharpBlogX.Domain.Messages;
using SharpBlogX.Domain.Sayings;
using SharpBlogX.Domain.Users;
using SharpBlogX.Dto.Blog;
using SharpBlogX.Dto.Hots;
using SharpBlogX.Dto.Messages;
using SharpBlogX.Dto.Sayings;
using SharpBlogX.Dto.Users;
using SharpBlogX.Extensions;

namespace SharpBlogX
{
    public class SharpBlogXApplicationAutoMapperProfile : Profile
    {
        public SharpBlogXApplicationAutoMapperProfile()
        {
            CreateMap<Post, PostDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<Post, PostDetailDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().FormatTime()));

            CreateMap<Post, PostBriefDto>()
                .ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().FormatTime()))
                .ForMember(x => x.Year, dto => dto.MapFrom(opt => opt.CreatedAt.Year));

            CreateMap<Post, GetAdminPostDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<Category, CategoryDto>();

            CreateMap<Category, GetAdminCategoryDto>();

            CreateMap<Category, CategoryAdminDto>();

            CreateMap<Tag, TagDto>();

            CreateMap<Tag, GetAdminTagDto>();

            CreateMap<Tag, TagAdminDto>();

            CreateMap<FriendLink, FriendLinkDto>();

            CreateMap<FriendLink, GetAdminFriendLinkDto>();

            CreateMap<Hot, HotSourceDto>();

            CreateMap<Hot, HotDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<Saying, SayingDto>();

            CreateMap<Message, MessageDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<MessageReply, MessageReplyDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<User, UserDto>().ForMember(x => x.CreatedAt, dto => dto.MapFrom(opt => opt.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}