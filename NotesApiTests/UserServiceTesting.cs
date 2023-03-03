using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NotesApi.Models;
using NotesApi.Models.Request;
using NotesApi.Models.Response;
using NotesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NotesApiTests
{
    public class UserServiceTesting
    {
        [Fact]
        public void Add_user_if_not_exists_and_data_ok()
        {
            // Arrange
            var data = new List<User>() { new User() { Email = "test1@gmail.com", Name="any1", Password = "123456" } }.AsQueryable();

            var userSet = new Mock<DbSet<User>>();
            userSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            userSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            userSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            userSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var dbMock = new Mock<NotesContext>();
            dbMock.Setup(x => x.Users).Returns(userSet.Object);

            var jwtConfig = Options.Create<JwtConfiguration>(new JwtConfiguration() { Secret = "sgg34g4g4hsbbxvbsdw" }); ;

            UserService _svc = new UserService(dbMock.Object, jwtConfig);

            SignUpRequest user = new SignUpRequest { Email= "test2@gmail.com", Name="any2", Password="123456" };

            //Act
            _svc.Add(user);

            //Asserts
            userSet.Verify(x => x.Add(It.IsAny<User>()), Times.Exactly(1));
            dbMock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Return_Token_if_user_exists()
        {
            // Arrange
            var data = new List<User>() { new User() { Email = "test2@gmail.com", Name = "a", Password = Encrypt.GetSHA256("123456") } }.AsQueryable();

            var userSet = new Mock<DbSet<User>>();
            userSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            userSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            userSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            userSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var dbMock = new Mock<NotesContext>();
            dbMock.Setup(x => x.Users).Returns(userSet.Object);

            var jwtConfig = Options.Create<JwtConfiguration>(new JwtConfiguration() { Secret= "sgg34g4g4hsbbxvbsdw" });

            UserService _svc = new UserService(dbMock.Object, jwtConfig);

            UserRequest user = new UserRequest { Email = "test2@gmail.com", Password = "123456" };

            //Act
            var response = _svc.Auth(user);

            //Asserts
            Assert.IsType<UserResponse>(response);
            Assert.Equal(response.Id, data.First().Id);
        }

        [Fact]
        public void Return_an_existent_user()
        {
            // Arrange
            var data = new List<User>() { new User() { Id=33, Email = "test2@gmail.com", Name = "a", Password = Encrypt.GetSHA256("123456") } }.AsQueryable();

            var userSet = new Mock<DbSet<User>>();
            userSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            userSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            userSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            userSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var dbMock = new Mock<NotesContext>();
            dbMock.Setup(x => x.Users).Returns(userSet.Object);

            var jwtConfig = Options.Create<JwtConfiguration>(new JwtConfiguration() { Secret = "sgg34g4g4hsbbxvbsdw" });

            UserService _svc = new UserService(dbMock.Object, jwtConfig);

            int userId = 33;

            //Act
            var response = _svc.Get(userId);

            //Asserts
            Assert.IsType<User>(response);
            Assert.Equal(userId, response.Id);
            Assert.Equal("test2@gmail.com", response.Email);
        }
    }
}
