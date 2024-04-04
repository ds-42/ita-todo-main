using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Users.Application.Dtos;

namespace Users.Application.Caches;

public class ApplicationUsersListMemoryCache : BaseCache<BaseListDto<GetUserDto>>;