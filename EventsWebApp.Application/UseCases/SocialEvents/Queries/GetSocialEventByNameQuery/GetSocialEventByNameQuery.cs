﻿using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public record GetSocialEventByNameQuery(string Name) : IQuery<SocialEventDto>;
}
