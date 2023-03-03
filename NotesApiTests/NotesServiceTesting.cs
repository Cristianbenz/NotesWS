
using DB;
using Microsoft.EntityFrameworkCore;
using Moq;
using NotesApi.Models.Request;
using NotesApi.Services;

namespace NotesApiTests
{
    public class NotesServiceTesting
    {
        private readonly NotesService _sut;
        private readonly Mock<NotesContext> _contextMock = new Mock<NotesContext>();

        public NotesServiceTesting()
        {
            _sut = new NotesService(_contextMock.Object);
        }

        [Fact]
        public void Return_user_notes_if_exists()
        {
            // Arrange
            var data = new List<Note>
            {
                new Note { Title = "test", Text = "test", UserId = 1 },
                new Note { Title = "test2", Text = "test2", UserId = 2 },
                new Note { Title = "test3", Text = "test3", UserId = 1},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Note>>();
            mockSet.As<IQueryable<Note>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Note>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Note>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Note>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<NotesContext>();
            mockContext.Setup(c => c.Note).Returns(mockSet.Object);

            var service = new NotesService(mockContext.Object);
            int userId = 1;

            // Act
            var response = service.GetNotes(userId);

            // Assert
            Assert.Equal(2, response.Count);
            Assert.Equal(1, response[1].UserId);
        }

        [Fact]
        public void Return_empty_list_if_user_does_not_exists()
        {
            // Arrange
            var data = new List<Note>
            {
                new Note { Title = "test", Text = "test", UserId = 1 },
                new Note { Title = "test2", Text = "test2", UserId = 2 },
                new Note { Title = "test3", Text = "test3", UserId = 1},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Note>>();
            mockSet.As<IQueryable<Note>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Note>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Note>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Note>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<NotesContext>();
            mockContext.Setup(c => c.Note).Returns(mockSet.Object);

            var service = new NotesService(mockContext.Object);
            int userId = It.IsAny<int>();

            // Act
            var response = service.GetNotes(userId);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void Return_new_note_if_user_exists_and_parameters_ok()
        {
            // Arrange
            var data = new List<User>
            {
                new User { Id = 1 }

            }.AsQueryable();

            var mockSetUser = new Mock<DbSet<User>>();
            mockSetUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockSetNote = new Mock<DbSet<Note>>();
            var mockContext = new Mock<NotesContext>();
            mockContext.Setup(c => c.Note).Returns(mockSetNote.Object);
            mockContext.Setup(c => c.Users).Returns(mockSetUser.Object);

            var service = new NotesService(mockContext.Object);
            NoteRequest note = new NoteRequest() { Title = "test", Text = "test", UserId = 1};

            // Act
            var response = service.AddNote(note);

            // Assert
            Assert.IsType<Note>(response);
        }
    }
}