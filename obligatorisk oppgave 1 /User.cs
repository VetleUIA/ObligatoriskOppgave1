namespace obligatorisk_oppgave_1;

public abstract class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email; 
        }

        public abstract string GetUserKey();    
    } 
    
