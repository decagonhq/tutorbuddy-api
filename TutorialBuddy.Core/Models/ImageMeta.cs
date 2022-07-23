using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core.Models
{
    public class ImageMeta: BaseEntity
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
    }
}
