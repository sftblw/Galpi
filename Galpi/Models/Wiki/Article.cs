using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Galpi.Models.Wiki
{
    public class Article
    {
        public string Id { get; set; }

        [MaxLength(512)]
        public string Title { get; set; }

        [MaxLength()] // max length
        public string Content { get; set; }

        public DateTime UpdatedDateTime { get; set; }
    }
}
