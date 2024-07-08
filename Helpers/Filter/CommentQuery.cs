using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers.Filter
{
    public class CommentQuery
    {
        public string? Title {get; set;} = null;
        public string? Content {get; set;} = null;
    }
}