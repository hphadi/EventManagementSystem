using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystemUI.Models
{
    public class NewUser
    {
        public string Name { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string RepeatPassword { get; set; } = "";

        public void Clear()
        {
            Name = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Name) || Name == "Enter name" ||
                string.IsNullOrWhiteSpace(UserName) || UserName == "Enter username" ||
                string.IsNullOrWhiteSpace(Password) || Password == "Enter password" ||
                string.IsNullOrWhiteSpace(RepeatPassword) || RepeatPassword == "Repeat password");
        }
    }

    public class LoginUser
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public void Clear()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(UserName) || UserName == "Enter username" ||
                string.IsNullOrWhiteSpace(Password) || Password == "Enter password");
        }
    }

    public class NewEventDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime? StartDateTime { get; set; } = null;
        public DateTime? EndDateTime { get; set; } = null;
        public string Location { get; set; } = "";


        public void Clear()
        {
            Title = string.Empty;
            Description = string.Empty;
            Location = string.Empty;
            StartDateTime = DateTime.Now;
            EndDateTime = DateTime.Now;
        }

        public bool IsValid()
        {
            return !(string.IsNullOrWhiteSpace(Title) || Title == "Enter name" ||
                string.IsNullOrWhiteSpace(Description) || Description == "Enter username" ||
                string.IsNullOrWhiteSpace(Location) || Location == "Enter password");
        }
    }
}
