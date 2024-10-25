using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;
using AutoMapper;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public class GetPaginatedSocialEventsHandler : IQueryHandler<GetPaginatedSocialEventsQuery, PaginatedList<SocialEventDto>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public GetPaginatedSocialEventsHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SocialEventDto>> Handle(GetPaginatedSocialEventsQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            (List<SocialEvent> socialEvents,int totalPages) = await _appUnitOfWork.SocialEventRepository.GetSocialEvents(request.Filters, request.PageIndex, request.PageSize, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            PaginatedList<SocialEventDto> responseList = new PaginatedList<SocialEventDto>(null, request.PageIndex, totalPages);
            responseList.Items = socialEvents.ConvertAll(_mapper.Map<SocialEventDto>).ToList();
            return responseList;
        }
    }
}
