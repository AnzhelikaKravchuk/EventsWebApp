namespace EventsWebApp.Application.Filters
{
    public record AppliedFilters(string? Name, 
                                    string? Date, 
                                    string? Place, 
                                    string? Category);
}