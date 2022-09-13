namespace SudokuApi.Models
{
    public class ResponseBody
    {
        public int status { get; set; }

        public string? message { get; set; }

        public Cell[]? cells { get; set; }
    }
}
