using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using Mediator;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsQuery : FilterRequestDto, IQuery<FilterResponseDto<Group>>
{

}
