namespace PluralHealthDemoApp.Models
{
    public class PagedResult<T>
    {
        public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; }
        public string ? Message { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public List<T> Items { get; set; } = [];
    }
}
