namespace obligatorisk_oppgave_1;

public abstract class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string name, string email, string username, string password)
        {
            Name = name;
            Email = email; 
            Username = username;
            Password = password;
        }

        public abstract string GetUserKey();    
    } 
    
