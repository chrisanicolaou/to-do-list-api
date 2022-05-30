using System;
using System.Collections.Generic;

namespace dotnet_backend
{
    public partial class ToDoItem
    {
        public int ToDoId { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool? IsActive { get; set; }
        public string DateCreated { get; set; } = null!;
        public string DateUpdated { get; set; } = null!;
        public int ArrayIndex { get; set; }

        public ToDoItem(string email, string desc, string dateCreated, int arrayIndex)
        {
            this.UserEmail = email;
            this.Description = desc;
            this.IsActive = true;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateCreated;
            this.ArrayIndex = arrayIndex;
        }
    }

    public class ToDoItemDTO
    {
        public string? DateUpdated { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public int? ArrayIndex { get; set; }
    }

    public class AddToDo
    {
        public string UserEmail { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DateCreated { get; set; } = null!;
        public int ArrayIndex { get; set; }
    }
}
