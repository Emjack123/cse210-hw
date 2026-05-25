using System;

namespace YouTubeTracker
{
    public class Comment
    {
        // Properties to track commenter details
        public string CommenterName { get; set; }
        public string CommentText { get; set; }

        // Constructor to easily initialize a comment
        public Comment(string commenterName, string commentText)
        {
            CommenterName = commenterName;
            CommentText = commentText;
        }
    }
}