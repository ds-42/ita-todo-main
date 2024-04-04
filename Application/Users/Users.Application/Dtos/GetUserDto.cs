using AutoMapper;
using Core.Application.Abstractions.Mappings;
using Core.Users.Domain;

namespace Users.Application.Dtos;

public class GetUserDto : IMapFrom<ApplicationUser>
{
    public Guid ApplicationUserId { get; set; }
    
    public string Login { get; set; } = default!;

    public int[] Roles { get; set; } = default!;

    public void CreateMap(Profile profile)
    {
        profile.CreateMap<ApplicationUser, GetUserDto>()
            .ForMember(e => e.Roles, r =>
                r.MapFrom(u => u.Roles.Select(s => s.ApplicationUserRoleId).ToArray()));
    }
}