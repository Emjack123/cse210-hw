using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Creating a list to store the videos
            List<Video> videoList = new List<Video>();

            // 2. Creating Video 1 and add comments
            Video video1 = new Video("C# Interfaces Explained", "CodeCraft", 620);
            video1.AddComment(new Comment("Alice", "Finally, a clear explanation of interfaces!"));
            video1.AddComment(new Comment("Bob", "The code examples were spot on. Thanks!"));
            video1.AddComment(new Comment("Charlie", "Can you do a video on abstract classes next?"));
            videoList.Add(video1);

            // 3. Creating Video 2 and add comments
            Video video2 = new Video("Top 5 CSS Layout Mistakes", "WebDevWizard", 455);
            video2.AddComment(new Comment("David", "Flex-box makes so much more sense now."));
            video2.AddComment(new Comment("Emma", "Guilty of mistake #3. Fixing my layout tonight."));
            video2.AddComment(new Comment("Frank", "Great production quality on this video."));
            video2.AddComment(new Comment("Grace", "Simple, concise, and highly informative."));
            videoList.Add(video2);

            // 4. Creating Video 3 and add comments
            Video video3 = new Video("Database Normalization (1NF, 2NF, 3NF)", "DataDesigners", 915);
            video3.AddComment(new Comment("Henry", "I actually understand 3NF for the first time in my life."));
            video3.AddComment(new Comment("Ivy", "Saved me right before my college midterm exam!"));
            video3.AddComment(new Comment("Jack", "Excellent breakdown of the anomalies."));
            videoList.Add(video3);

            // 5. Iterating through the list of videos and display the data
            Console.WriteLine("========================================");
            Console.WriteLine("       YOUTUBE VIDEO TRACKER REPORT     ");
            Console.WriteLine("========================================\n");

            foreach (Video video in videoList)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
                Console.WriteLine($"Total Comments: {video.GetCommentCount()}");
                Console.WriteLine("Comments:");

                // Iterating through the comments of the current video
                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"  - {comment.CommenterName}: \"{comment.CommentText}\"");
                }

                Console.WriteLine("\n----------------------------------------\n");
            }
        }
    }
}