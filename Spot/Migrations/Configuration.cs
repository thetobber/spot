using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Spot.Models;

namespace Spot.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDbContext context)
        {
            // Update-Database -Verbose -StartUpProjectName Spot.Data

            //Remove everything
            context.Posts.RemoveRange(context.Posts);
            context.Comments.RemoveRange(context.Comments);
            context.Tags.RemoveRange(context.Tags);
            context.SaveChanges();

            //Reset primary key counter on posts and comments table
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Posts', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Comments', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Tags', RESEED)");

            List<Post> posts = new List<Post> {
                new Post {
                    Title = "Lorem ipsum",
                    Excerpt = "Etiam ut magna vitae ex rhoncus pulvinar. Aliquam erat volutpat.",
                    Content = "Etiam ut magna vitae ex rhoncus pulvinar. Aliquam erat volutpat.",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Published = DateTime.Now
                },
                new Post {
                    Title = "Aenean sit amet",
                    Excerpt = "Mauris id ullamcorper risus. Suspendisse consectetur ipsum ac fermentum condimentum.",
                    Content = "Mauris id ullamcorper risus. Suspendisse consectetur ipsum ac fermentum condimentum.",
                    Created = DateTime.Now.AddDays(-1),
                    Modified = DateTime.Now.AddDays(-1),
                    Published = DateTime.Now.AddDays(-1)
                },
                new Post {
                    Title = "Mauris id ullamcorper",
                    Excerpt = "Aenean sit amet placerat leo, ut rhoncus orci. Aenean imperdiet eget massa vel convallis.",
                    Content = "Aenean sit amet placerat leo, ut rhoncus orci. Aenean imperdiet eget massa vel convallis.",
                    Created = DateTime.Now.AddDays(-2),
                    Modified = DateTime.Now.AddDays(-2),
                    Published = DateTime.Now.AddDays(-2)
                },
                new Post {
                    Title = "Etiam ut magna vitae",
                    Excerpt = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Created = DateTime.Now.AddDays(-3),
                    Modified = DateTime.Now.AddDays(-3),
                    Published = DateTime.Now.AddDays(-3)
                }
            };

            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();
        }
    }
}