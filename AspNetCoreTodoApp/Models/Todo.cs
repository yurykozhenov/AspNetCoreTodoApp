using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodoApp.Models
{
    public class Todo
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
