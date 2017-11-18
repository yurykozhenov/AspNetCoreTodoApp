using AspNetCoreTodoApp.Controllers;
using AspNetCoreTodoApp.Models;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodoApp.Tests.UnitTests.Controllers
{
    public class TodosControllerTest
    {
        [Fact]
        public void GetTodosTest()
        {
            var mockSet = new Mock<DbSet<Todo>>();
            //mockSet.SetupData(GetTestTodos());  

            var mockContext = new Mock<TodoContext>();
            mockContext.Setup(c => c.Todos).Returns(() => mockSet.Object);

            var controller = new TodosController(mockContext.Object);

            var result = controller.GetTodos();

            Assert.NotNull(result);
        }

        private List<Todo> GetTestTodos()
        {
            var todos = new List<Todo>
            {
                new Todo()
                {
                    Id = 1,
                    Name = "Todo1"
                },

                new Todo()
                {
                    Id = 2,
                    Name = "Todo2",
                    IsComplete = true
                }
            };

            return todos;
        }
    }
}