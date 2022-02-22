namespace RepCrimeCommon.Models;

public class QueryParameters
{
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime StopDate { get; set; } = DateTime.Now;
    public bool Descending { get; set; } = false;
    const int maxPageSize = 100;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 3;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > maxPageSize)
            {
                _pageSize = maxPageSize;
            }
            else if (value < 1)
            {
                _pageSize = 1;
            }
        }
    }
}
