using LabReserve.Domain.Dto;
using LabReserve.Domain.Entities;
using MediatR;

namespace LabReserve.Application.UseCases.Groups.GetGroups;

public class GetGroupsQuery : FilterRequestDto, IRequest<FilterResponseDto<Group>>
{

}
