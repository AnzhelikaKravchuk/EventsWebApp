using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Filters;
using EventsWebApp.Domain.PaginationHandlers;

namespace EventsWebApp.Application.SocialEvents.Queries
{
    public record GetPaginatedSocialEventsQuery(AppliedFilters Filters, int PageIndex = 1, int PageSize = 10) : IQuery<PaginatedList<SocialEventDto>>;
}
