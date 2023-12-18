using System.Collections;
using Domain;

namespace MoodServer.Tests.DomainUnitTests;

public class UserTests
{
     [Test]
     public void FriendCount_ReturnsCorrectCount()
     {
         // Arrange
         var user = new User();
         var friend1 = new User { Id = "1" };
         var friend2 = new User { Id = "2" };
    
         // Act
         user.Add(friend1);
         user.Add(friend2);
    
         // Assert
         Assert.That(user.FriendCount, Is.EqualTo(2));
    }
     
     [Test]
     public void Add_Friend_IncreasesFriendCount()
     {
         // Arrange
         var user = new User();
         var friend = new User { Id = "1" };

         // Act
         user.Add(friend);

         // Assert
         Assert.That(user.FriendCount, Is.EqualTo(1));
     }
     
     [Test]
     public void Add_Publication_IncreasesPublicationCount()
     {
         // Arrange
         var user = new User();
         var publication = new Publication { Id = 1 };

         // Act
         user.Add(publication);

         // Assert
         Assert.That(user.PublicationCount, Is.EqualTo(1));
     }
     
     [Test]
     public void Add_ExistingFriend_DoesNotIncreaseFriendCount()
     {
         // Arrange
         var user = new User();
         var friend = new User { Id = "1" };
         user.Add(friend);

         // Act
         user.Add(friend);

         // Assert
         Assert.That(user.FriendCount, Is.EqualTo(1));
     }
     
     [Test]
     public void Add_ExistingPublication_DoesNotIncreasePublicationCount()
     {
         // Arrange
         var user = new User();
         var publication = new Publication { Id = 1 };
         user.Add(publication);

         // Act
         user.Add(publication);

         // Assert
         Assert.That(user.PublicationCount, Is.EqualTo(1));
     }
     
     [Test]
     public void Friends_ReturnsCorrectList()
     {
         // Arrange
         var user = new User();
         var friend1 = new User { Id = "1" };
         var friend2 = new User { Id = "2" };
         user.Add(friend1);
         user.Add(friend2);

         // Act
         var friends = user.Friends();

         // Assert
         Assert.That(friends.Count(), Is.EqualTo(2));
         Assert.Contains(friend1, (ICollection?)friends);
         Assert.Contains(friend2, (ICollection?)friends);
     }
     
     [Test]
     public void Publications_ReturnsCorrectList()
     {
         // Arrange
         var user = new User();
         var publication1 = new Publication { Id = 1 };
         var publication2 = new Publication { Id = 2 };
         user.Add(publication1);
         user.Add(publication2);

         // Act
         var publications = user.Publications();

         // Assert
         Assert.That(publications.Count(), Is.EqualTo(2));
         Assert.Contains(publication1, (ICollection?)publications);
         Assert.Contains(publication2, (ICollection?)publications);
     }
     
     [Test]
     public void AddRange_Friends_AddsAllFriends()
     {
         // Arrange
         var user = new User();
         var friend1 = new User { Id = "1" };
         var friend2 = new User { Id = "2" };
         var friends = new User[] { friend1, friend2 };

         // Act
         user.AddRange(friends);

         // Assert
         Assert.That(user.FriendCount, Is.EqualTo(2));
     }
     
     [Test]
     public void AddRange_Publications_AddsAllPublications()
     {
         // Arrange
         var user = new User();
         var publication1 = new Publication { Id = 1 };
         var publication2 = new Publication { Id = 2 };
         var publications = new Publication[] { publication1, publication2 };

         // Act
         user.AddRange(publications);

         // Assert
         Assert.That(user.PublicationCount, Is.EqualTo(2));
     }
     
     [Test]
     public void ToString_ReturnsCorrectString()
     {
         // Arrange
         var user = new User { Id = "1", Name = "John Doe" };

         // Act
         var userString = user.ToString();

         // Assert
         Assert.IsTrue(userString.Contains("User"));
         Assert.IsTrue(userString.Contains("Id=1"));
         Assert.IsTrue(userString.Contains("Name=John Doe"));
     }
     
}