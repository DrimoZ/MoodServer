using Domain;

namespace MoodServer.Tests;

[TestFixture]
public class PublicationTests
{
    [Test]
    public void Add_SingleComment_IncreasesCommentCount()
    {
        // Arrange
        var publication = new Publication();
        var comment = new Comment { CommentId = 1 };

        // Act
        publication.Add(comment);

        // Assert
        Assert.That(publication.CommentCount, Is.EqualTo(1));
    }
    
    [Test]
    public void AddRange_MultipleComments_IncreasesCommentCount()
    {
        // Arrange
        var publication = new Publication();
        var comments = new List<Comment>
        {
            new Comment { CommentId = 1 },
            new Comment { CommentId = 2 },
            new Comment { CommentId = 3 }
        };

        // Act
        publication.AddRange(comments);

        // Assert
        Assert.That(publication.CommentCount, Is.EqualTo(3));
    }
}