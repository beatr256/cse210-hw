using System;
using System.Collections.Generic;

// Define Comment class
class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Define Video class
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("C# Basics Tutorial", "John Doe", 600);
        Video video2 = new Video("Learn Unity in 30 Minutes", "Jane Smith", 1800);
        Video video3 = new Video("Top 10 Coding Tips", "Alex Johnson", 900);

        // Add comments to video 1
        video1.AddComment(new Comment("Alice", "Great tutorial!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "I was stuck on this, now I get it."));

        // Add comments to video 2
        video2.AddComment(new Comment("Daisy", "Love Unity!"));
        video2.AddComment(new Comment("Ethan", "This really sped up my learning."));
        video2.AddComment(new Comment("Fiona", "Could you do more on animations?"));

        // Add comments to video 3
        video3.AddComment(new Comment("George", "Awesome tips!"));
        video3.AddComment(new Comment("Hannah", "I learned so much from this."));
        video3.AddComment(new Comment("Ian", "Thanks for sharing!"));

        // Put videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video info and comments
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }
            Console.WriteLine(); // Blank line for separation
        }
    }
}
