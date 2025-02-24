using System;
using System.Collections.Generic;

namespace Enframework_learning_2
{

    public partial class Blogimage
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public int?Blogforignkey { get; set; }

        public Blog blog { get; set; }

        public List<Post> Posts { get; set; }= new List<Post>();
    }
}
