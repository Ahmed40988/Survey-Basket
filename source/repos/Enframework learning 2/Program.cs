
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Enframework_learning_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            //seedingData();
            DbcontextEfcoreold context = new DbcontextEfcoreold();
            #region select all
            //context.stocks.ToList();
            //foreach (var stock in context.stocks)
            //    Console.WriteLine(stock.Name);
            #endregion

            #region select element
            //var stock= context.stocks.Find(100);
            //     Console.WriteLine($"stockId:{stock.Id}\nstockName:{stock.Name}");
            #endregion

            #region append(),Prepend()
            //// add new stock in client site not server site not in database 
            //var stock = context.stocks.Where(p => p.Id > 500).ToList().Append(new Stock { Id = 1001, Name = "test" });
            //var stock2 = context.stocks.Where(p => p.Id > 500).ToList().Prepend(new Stock { Id = 1001, Name = "test" });
            //foreach (var item in stock2) 
            //Console.WriteLine($"{item.Id},{item.Name}");

            #endregion

            #region Average,count,sum
            //var stock = context.stocks.Where(s => s.Id > 900).Average(s => s.Balance);
            //var stock2 = context.stocks.Count(p => p.Id > 500);
            //var stock3 = context.stocks.LongCount(p => p.Id > 500);// for big data 
            //var stock4 = context.stocks.Where(p => p.Id > 500).Sum(p=>p.Balance);
            //Console.WriteLine(stock4);
            #endregion

            #region Sorting==>orderby,orderbyDesending,thenby,thenbyDesending
            //var stock=context.stocks.OrderBy(x=> x.Balance).ToList();

            //// first sort by industry and sort every item in each industry by balance
            //var stock1=context.stocks.OrderBy(x=> x.Industry).ThenBy(x=>x.Balance).ToList();
            //foreach(var x in  stock1)
            //    Console.WriteLine($"{x.Industry}__{x.Balance}");
            #endregion

            #region select, projection==>change form of data "تغيير شكل الداتا اللي راجعة "
            //// normal select but changename of columns in return data===>anounmes name
            //var stock = context.stocks.Select(m => new { stock_id = m.Id, stockname = m.Name });
            //// change to post
            //var stock1 = context.stocks.Select(m => new Post { Id = m.Id,  Content= m.Name });
            //foreach(var s in stock1) 
            //    Console.WriteLine($"{s.Id}: {s.Content}");
            #endregion

            #region Group by
            //var stock = context.stocks.GroupBy(x => x.Industry)
            //    .Select(x => new { industry = x.Key, count = x.Count() }).OrderBy(x => x.count).ToList(); //key= industry
            //var stock1 = context.stocks.GroupBy(x => x.Industry)
            //    .Select(x => new { industryname = x.Key, countitem = x.Count(), sumbalance = x.Sum(p => p.Balance) });
            //foreach (var x in stock1)
            //    Console.WriteLine($"{x.industryname}======>{x.countitem}======>{x.sumbalance}");
            #endregion

            #region Join between two tables and return data from two tables or join with many tables

            //// join two tables only


            // var res1 = context.Blogs
            //     .Join(
            //    context.Post,
            //    Blog => Blog.Id,
            //    Post => Post.Blogid,
            //    (Blog, Post) =>
            //    new
            //    {
            //        Blog_id = Blog.Id,
            //        post_id = Post.Id,
            //        Blog_URL = Blog.Url,
            //        Post_content = Post.Content,
            //        Post_title = Post.Title,
            //    });

            //  //in this join it will join Blogimage and res2(anounmes name) and access on res2 by name of base table ==>Blog
            // var res2 = context.Blogs
            //  .Join(
            // context.Post,
            // Blog => Blog.Id,
            // Post => Post.Blogid,
            // (Blog, Post) =>
            // new
            // {
            //     Blog_id = Blog.Id,
            //     post_id = Post.Id,
            //     Blog_URL = Blog.Url,
            //     Post_title = Post.Title,
            // }) 
            //  .Join(
            //     context.blogimage,
            //     Blog=>Blog.Blog_id,
            //     Blogimage=>Blogimage.Blogforignkey,
            //     (Blog, Blogimage) =>
            //     new
            //     {
            //         Blog.Blog_id,
            //         Blog.Blog_URL,
            //         Blogimage.Blogforignkey,
            //         blogtitle=Blog.Post_title,
            //        image=Blogimage.Image,

            //     });
            // //left join by Grouping Join
            // var res3 = context.Blogs
            //     .GroupJoin(
            //    context.Post,
            //    Blog => Blog.Id,
            //    Post => Post.Blogid,
            //    (Blog, Post) =>
            //    new
            //    {
            //        blog = Blog,
            //        post = Post,

            //    }).SelectMany(b => b.post.DefaultIfEmpty(), (B, P) => new { B.blog, p=P.Title });

            // foreach ( var x in res3 ) 
            //     Console.WriteLine($"{x.blog.Id}==>{x.blog.Url}==>{x.p}" );
            #endregion

            #region Eager Loading
            //var blog = context.Blogs.Include(b => b.Blogimage).FirstOrDefault(b => b.Id == 1);
            //Console.WriteLine($"{blog.Blogimage.Image} - {blog.Url}");
            // or by use join inner
            //var blog2 = context.Blogs.Join
            //    (
            //    context.blogimage,
            //    Blog=>Blog.Id,
            //    Blogimage=>Blogimage.Blogforignkey,
            //    (Blog, Blogimage) => new
            //    {
            //        Blog.Id,
            //        Blogimagename=Blogimage.Image,
            //        Blogurl=Blog.Url,
            //    }).FirstOrDefault(b => b.Id == 1);
            //Console.WriteLine($"{blog2.Blogimagename} - {blog2.Blogurl}");
            #endregion

            #region Explicit Loading==> return related Data on two steps     Very important
            // first step ==>return base data from first table
            //var Blog1 = context.Blogs.FirstOrDefault(B => B.Id == 2);

            // 2nd step ==>return relted data from anthor
            // Table if relation one to one"object not Collection"
            //context.Entry(Blog1)
            //    .Reference(b => b.Blogimage)
            //    .Load();
            //Console.WriteLine($"{Blog1.Blogimage.Image} - {Blog1.Url}");


            // 2nd step ==>return relted data from anthor Table if relation one to many "Collection"
            //context.Entry(Blog1).Collection(b => b.Posts).Load();

            ////foreach (var b in Blog1.Posts)
            ////    Console.WriteLine($"{b.Title} - {Blog1.Id}");

            //// or add condition

            //context.Entry(Blog1)
            //    .Collection(b => b.Posts)
            //    .Query()
            //    .Where(p => p.Id > 2).ToList();  // add condition postid>2


            //foreach (var b in Blog1.Posts)
            //    Console.WriteLine($"{b.Title} - {Blog1.Id}");
            #endregion


            #region add data in table that is related anothor table

            //var blog = new Blog()
            //{
            //    Url = "www.new link1",
            //    DateTime = DateTime.Now,
            //};
            //////////////////////////////////////////////
            //var blogimage = new Blogimage()
            //{
            //    Image = "new image 2",
            //    blog = new Blog()
            //    {
            //        Url="link1717",
            //        DateTime = DateTime.Now,
            //    },
            //    Posts = new List<Post>()
            //    {
            //      new Post{Posturl="url 717",Content="new content 1717",Title="new title 555" ,Blogid=5},
            //      new Post{Posturl="url 717",Content="new content 1717",Title="new title 555" ,Blogid=5},
            //      new Post{Posturl="url 717",Content="new content 1717",Title="new title 555" ,Blogid=5},
            //      new Post{Posturl="url 717",Content="new content 1717",Title="new title 555" ,Blogid=5},

            //    },
            //};

            //////////////////////////////////////////////
            var blogimage = new Post()
            {
                Posturl = "url 2020",
                Content = "new content 2020",
                Title = "new title 2020",
               blogimage=new Blogimage()
               {
                    Image="image 121212",
                    blog=new Blog()
                    {
                        Url="link1313131",
                        DateTime=DateTime.Now
                    },
                    
               },
             Blog=new Blog()
             {
                 Url="link141414",
                 DateTime = DateTime.Now 
             },
             
            

            };
            context.Add(blogimage);
            context.SaveChanges();
            #endregion

            #region
            #endregion

        }

        // seeding Data ==> auto insert intial or constant Data when run programmme 
        public static void seedingData()
        {
            //take obj from Dbcontext to access database
            using (var context = new DbcontextEfcoreold())
            {
                // ensure the database is created or not if not created it will create
                context.Database.EnsureCreated();
                //check the data is inserted or not 
                var find = context.Blogs.FirstOrDefault(b => b.Url == "wwww.Goole.come");
                // insert data
                if (find == null)
                {
                    context.Blogs.Add(new Blog { Url = "wwww.Goole.come" });
                    context.SaveChanges();
                    Console.WriteLine("insert is succesed");
                }

            }

        }
    }
}
