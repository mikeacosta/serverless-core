using System.Collections.Generic;

namespace ServerlessCore.Data.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string Website { get; set; }

        public string City { get; set; }

        public string Image { get; set; }

        public List<string> Content { get; set; }
    }
}
