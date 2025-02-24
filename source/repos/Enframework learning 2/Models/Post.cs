using System;
using System.Collections.Generic;
namespace Enframework_learning_2
{ 
public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string ?Posturl { get; set; }

    public string ?Content { get; set; }

    public int? Blogid { get; set; }
    public int? Blogimageid { get; set; }

     public Blogimage blogimage { get; set; }

    public Blog Blog { get; set; }
}
}
