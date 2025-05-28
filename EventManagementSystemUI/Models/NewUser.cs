using System.Windows;

namespace EventManagementSystemUI.Models
{

    public static class ErrorMessages
    {
        public const string NameRequired = "Name is required.";
        public const string NameTooShort = "Name must be at least 5 characters.";
        public const string UserNameRequired = "Username is required.";
        public const string UserNameTooShort = "Username must be at least 5 characters.";
        public const string PasswordRequired = "Password is required.";
        public const string PasswordTooShort = "Password must be at least 5 characters.";
        public const string PasswordsDoNotMatch = "Passwords do not match.";
        public const string RepeatPasswordRequired = "Please repeat the password.";

        public const string TitleRequired = "Title is required.";
        public const string TitleTooShort = "Title must be at least 5 characters.";

        public const string DescriptionRequired = "Description is required.";
        public const string DescriptionTooShort = "Description must be at least 10 characters.";

        public const string StartDateRequired = "Start date is required.";
        public const string StartDateInPast = "Start date cannot be in the past.";

        public const string EndDateRequired = "End date is required.";
        public const string EndDateInPast = "End date cannot be in the past.";
        public const string EndBeforeStart = "End date must be after start date.";

        public const string LocationRequired = "Location is required.";
        public const string LocationTooShort = "Location must be at least 2 characters.";
        public const string CityRequired = "City is required.";
        public const string CityTooShort = "City must be at least 10 characters.";
    }
    public interface IDraftClassForm
    {
        void Clear();
    }

    public static class ValidationExtensions
    {
        public static bool ShowValidationErrorsIfInvalid(this ValidityEntity entity)
        {
            var errors = entity.GetValidationErrors();
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
    public abstract class ValidityEntity
    {
        public abstract List<string> GetValidationErrors();

    }


    public class NewUser : ValidityEntity, IDraftClassForm
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

        public override List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name) || Name == "Enter name")
                errors.Add(ErrorMessages.NameRequired);

            else if (string.IsNullOrWhiteSpace(UserName) || UserName == "Enter username")
                errors.Add(ErrorMessages.UserNameRequired);
            else if (UserName.Length < 5)
                errors.Add(ErrorMessages.UserNameTooShort);

            else if (string.IsNullOrWhiteSpace(Password) || Password == "Enter password")
                errors.Add(ErrorMessages.PasswordRequired);
            else if (Password.Length < 5)
                errors.Add(ErrorMessages.PasswordTooShort);

            else if (string.IsNullOrWhiteSpace(RepeatPassword) || RepeatPassword == "Repeat password")
                errors.Add(ErrorMessages.RepeatPasswordRequired);
            else if (RepeatPassword != Password)
                errors.Add(ErrorMessages.PasswordsDoNotMatch);
            return errors;
        }
    }

    public class LoginUser : ValidityEntity, IDraftClassForm
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public void Clear()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        public override List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(UserName) || UserName == "Enter username")
                errors.Add(ErrorMessages.UserNameRequired);


            else if (string.IsNullOrWhiteSpace(Password) || Password == "Enter password")
                errors.Add(ErrorMessages.PasswordRequired);
            return errors;
        }
        //public bool IsValid()
        //{
        //    return !(string.IsNullOrWhiteSpace(UserName) || UserName == "Enter username" ||
        //        string.IsNullOrWhiteSpace(Password) || Password == "Enter password");
        //}
    }

    public class NewEventDto : ValidityEntity, IDraftClassForm
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

        public override List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Title) || Title == "Enter title")
                errors.Add(ErrorMessages.TitleRequired);
            else if (Title.Length < 5)
                errors.Add(ErrorMessages.TitleTooShort);
            else if (string.IsNullOrWhiteSpace(Description) || Description == "Enter description")
                errors.Add(ErrorMessages.DescriptionRequired);
            else if (Description.Length < 10)
                errors.Add(ErrorMessages.DescriptionTooShort);
            else if (!StartDateTime.HasValue)
                // StartDate is null (not set)
                errors.Add(ErrorMessages.StartDateRequired);
            else if (StartDateTime.Value < DateTime.Now)
                errors.Add(ErrorMessages.StartDateInPast);
            else if (!EndDateTime.HasValue)
                errors.Add(ErrorMessages.EndDateRequired);
            else if (EndDateTime.Value < DateTime.Now)
                errors.Add(ErrorMessages.EndDateInPast);
            else if (EndDateTime <= StartDateTime)
                errors.Add(ErrorMessages.EndBeforeStart);
            else if (string.IsNullOrWhiteSpace(Location) || Location == "Enter location")
                errors.Add(ErrorMessages.LocationRequired);
            else if (Location.Length < 10)
                errors.Add(ErrorMessages.LocationTooShort);

            return errors;
        }
        //public bool IsValid()
        //{
        //    return !(string.IsNullOrWhiteSpace(Title) || Title == "Enter title" ||
        //        string.IsNullOrWhiteSpace(Description) || Description == "Enter description" ||
        //        string.IsNullOrWhiteSpace(Location) || Location == "Enter location");
        //}
    }

    public class DraftGroupDto : ValidityEntity, IDraftClassForm
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string City { get; set; } = "";


        public void Clear()
        {
            Name = string.Empty;
            Description = string.Empty;
            City = string.Empty;
        }

        public override List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name) || Name == "Enter name")
                errors.Add(ErrorMessages.NameRequired);
            else if (Name.Length < 5)
                errors.Add(ErrorMessages.NameTooShort);

            else if (string.IsNullOrWhiteSpace(City) || City == "Enter city")
                errors.Add(ErrorMessages.CityRequired);
            else if (City.Length < 2)
                errors.Add(ErrorMessages.CityTooShort);

            else if (string.IsNullOrWhiteSpace(Description) || Description == "Enter description")
                errors.Add(ErrorMessages.DescriptionRequired);
            else if (Description.Length < 10)
                errors.Add(ErrorMessages.DescriptionTooShort);

            return errors;
        }
        //public bool IsValid()
        //{
        //    return !(string.IsNullOrWhiteSpace(Name) || Name == "Enter name" ||
        //        string.IsNullOrWhiteSpace(Description) || Description == "Enter description" ||
        //        string.IsNullOrWhiteSpace(City) || City == "Enter city");
        //}
    }
}
