using System.Collections.Generic;
using System.Linq;
using ShoppingList.Models;

namespace ShoppingList.Pages
{
    public partial class Index
    {
        private List<TodoItem> todos = new();
        private int TodosToDo => todos.Count(t => !t.IsDone);
        private string newTodo;

        private void AddTodo()
        {
            if (string.IsNullOrWhiteSpace(newTodo))
            {
                return;
            }

            todos.Add(new TodoItem { Title = newTodo });
            newTodo = string.Empty;
        }
    }
}
