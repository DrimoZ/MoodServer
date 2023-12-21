using System.Collections;
using Domain;

namespace MoodServer.Tests.DomainUnitTests;

public class PublicationTests
{
    [Test]
    public void Add_Comment_IncreasesCommentCount()
    {
        // Arrange
        var publication = new Publication();
        var comment1 = new Comment { Id = 1 };
        var comment2 = new Comment { Id = 2 };

        // Act
        publication.Add(comment1);
        publication.Add(comment2);

        // Assert
        Assert.That(publication.CommentCount, Is.EqualTo(2));
    }
    
    [Test]
    public void Add_Like_IncreasesLikeCount()
    {
        // Arrange
        var publication = new Publication();
        var like1 = new Like { Id = 1 };
        var like2 = new Like { Id = 2 };

        // Act
        publication.Add(like1);
        publication.Add(like2);

        // Assert
        Assert.That(publication.LikeCount, Is.EqualTo(2));
    }
    
    [Test]
    public void Add_ExistingComment_DoesNotIncreaseCommentCount()
    {
        // Arrange
        var publication = new Publication();
        var comment = new Comment { Id = 1 };
        publication.Add(comment);

        // Act
        publication.Add(comment);

        // Assert
        Assert.That(publication.CommentCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Add_ExistingLike_DoesNotIncreaseLikeCount()
    {
        // Arrange
        var publication = new Publication();
        var like = new Like { Id = 1 };
        publication.Add(like);

        // Act
        publication.Add(like);

        // Assert
        Assert.That(publication.LikeCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Comments_ReturnsCorrectList()
    {
         // Arrange
         var publication = new Publication();
         var comment1 = new Comment { Id = 1 };
         var comment2 = new Comment { Id = 2 };
         publication.Add(comment1);
         publication.Add(comment2);

         // Act
         var comments = publication.Comments();

         // Assert
         Assert.That(comments.Count(), Is.EqualTo(2));
         Assert.Contains(comment1, (ICollection?)comments);
         Assert.Contains(comment2, (ICollection?)comments);
    }
         
     [Test]
     public void Likes_ReturnsCorrectList()
     {
         // Arrange
         var publication = new Publication();
         var like1 = new Like { Id = 1 };
         var like2 = new Like { Id = 2 };
         publication.Add(like1);
         publication.Add(like2);

         // Act
         var likes = publication.Likes();

         // Assert
         Assert.That(likes.Count(), Is.EqualTo(2));
         Assert.Contains(like1, (ICollection?)likes);
         Assert.Contains(like2, (ICollection?)likes);
     }
     
     [Test]
     public void AddRange_Comments_AddsAllComments()
     {
         // Arrange
         var publication = new Publication();
         var comment1 = new Comment { Id = 1 };
         var comment2 = new Comment { Id = 2 };
         var comments = new Comment[] { comment1, comment2 };

         // Act
         publication.AddRange(comments);

         // Assert
         Assert.That(publication.CommentCount, Is.EqualTo(2));
     }
     
     [Test]
     public void AddRange_Likes_AddsAllLikes()
     {
         // Arrange
         var publication = new Publication();
         var like1 = new Like { Id = 1 };
         var like2 = new Like { Id = 2 };
         var likes = new Like[] { like1, like2 };

         // Act
         publication.AddRange(likes);

         // Assert
         Assert.That(publication.LikeCount, Is.EqualTo(2));
     }
     
     [Test]
     public void Elements_ReturnsCorrectList()
     {
         // Arrange
         var publication = new Publication();
         var element1 = new PublicationElement { Id = 1 };
         var element2 = new PublicationElement { Id = 2 };
         publication.Elements = new PublicationElement[] { element1, element2 };

         // Act
         var elements = publication.Elements;

         // Assert
         Assert.That(elements.Count(), Is.EqualTo(2));
         Assert.Contains(element1, (ICollection?)elements);
         Assert.Contains(element2, (ICollection?)elements);
     }
}