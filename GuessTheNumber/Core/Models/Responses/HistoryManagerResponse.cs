using System.Collections.Generic;

namespace Core.Models.Responses
{
    public class HistoryManagerResponse : GameManagerResponse
    {
        public List<UserGame> History { get; set; }
    }
}