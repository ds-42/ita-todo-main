using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Todos.Applications.DTOs;

namespace Todos.Applications.Caches;

public class TodosListMemoryCache : BaseCache<BaseListDto<GetTodoDto>>;