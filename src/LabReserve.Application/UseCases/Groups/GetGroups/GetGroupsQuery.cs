using LabReserve.Domain.Dto;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsQuery : FilterRequestDto, IQuery<FilterResponseDto<GroupResponse>>;
