namespace ThesesApi;

public class ThesisTableItemResourceDataTableResult
{
    public int TotalItems { get; set; }

    public int Page {  get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public List<ThesisTableItemResource>? Items { get; set; }

}