namespace EventsWebApp.Application.Filters
{
    public record AppliedFilters
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Place { get; set; }
        public string? Date { get; set; }
        public AppliedFilters()
        {
        }

        public AppliedFilters(string? name, string? category, string? place, string? date)
        {
            Name = name;
            Category = category;
            Place = place;
            Date = date;
        }
    }
}