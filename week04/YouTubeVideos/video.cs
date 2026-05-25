using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    public class Video
    {
        // Core video attributes
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthInSeconds { get; set; }

        // List to store tracking comments
        private List<Comment> _comments;

        // Constructor
        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            _comments = new List<Comment>();
        }

        // Method to add a comment to this specific video
        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        // Method that returns the total number of comments
        public int GetCommentCount()
        {
            return _comments.Count;
        }

        //  retrieving the list of comments for iteration in Main
        public List<Comment> GetComments()
        {
            return _comments;
        }
    }
}
