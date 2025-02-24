using System;
using System.Collections.Generic;

namespace Enframework_learning_2
{ 
public partial class Blog
{
    public int Id { get; set; }

     public string Url { get; set; } = null!;

        public DateTime DateTime { get; set; }

    public Blogimage? Blogimage { get; set; }

    public List<Post> Posts { get; set; } = new List<Post>();
}
}