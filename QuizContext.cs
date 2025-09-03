using Microsoft.EntityFrameworkCore;
using StepIT_ADO.NET_FinalProject.Classes;

namespace StepIT_ADO.NET_FinalProjectl;

internal class QuizContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Result> Results { get; set; }

    public QuizContext(string connectionString)
    {
        _connectionString = connectionString;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder
            .Entity<User>()
            .Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(25);

        modelBuilder
            .Entity<User>()
            .HasIndex(x => x.Username)
            .IsUnique();

        modelBuilder
            .Entity<User>()
            .Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(25);

        modelBuilder
            .Entity<User>()
            .ToTable(x => x.HasCheckConstraint("CK_User_Birthdate",
            "DATEDIFF(YEAR, Birthdate, GETDATE()) >= 8"));

        // Category
        modelBuilder
            .Entity<Category>()
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder
            .Entity<Category>()
            .HasMany(x => x.Questions)
            .WithOne(q => q.Category)
            .HasForeignKey(q => q.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Question
        modelBuilder
            .Entity<Question>()
            .Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder
            .Entity<Question>()
            .HasMany(x => x.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Asnwer
        modelBuilder
            .Entity<Answer>()
            .Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(255);

        // Result
        modelBuilder
            .Entity<Result>()
            .HasOne(r => r.User)
            .WithMany(u => u.Results)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Result>()
            .HasOne(r => r.Category)
            .WithMany(q => q.Results)
            .HasForeignKey(r => r.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        // Categories
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Mathematics" },
            new { Id = 2, Name = "Computer Science" },
            new { Id = 3, Name = "Chemistry" }
        );

        // Questions
        modelBuilder.Entity<Question>().HasData(
            // Mathematics Questions (1-30)
            new { Id = 1, Text = "What is the value of π (pi) rounded to 2 decimal places?", CategoryId = 1 },
            new { Id = 2, Text = "What is the square root of 144?", CategoryId = 1 },
            new { Id = 3, Text = "If x + 5 = 12, what is the value of x?", CategoryId = 1 },
            new { Id = 4, Text = "What is 15% of 200?", CategoryId = 1 },
            new { Id = 5, Text = "What is the sum of angles in a triangle?", CategoryId = 1 },
            new { Id = 6, Text = "What is 2⁴ (2 to the power of 4)?", CategoryId = 1 },
            new { Id = 7, Text = "What is the derivative of x²?", CategoryId = 1 },
            new { Id = 8, Text = "What is the area of a circle with radius 5?", CategoryId = 1 },
            new { Id = 9, Text = "What is the value of log₁₀(100)?", CategoryId = 1 },
            new { Id = 10, Text = "What is the slope of the line y = 3x + 2?", CategoryId = 1 },
            new { Id = 11, Text = "What is 7! (7 factorial)?", CategoryId = 1 },
            new { Id = 12, Text = "What is the next number in the sequence: 2, 4, 8, 16, ...?", CategoryId = 1 },
            new { Id = 13, Text = "What is the hypotenuse of a right triangle with sides 3 and 4?", CategoryId = 1 },
            new { Id = 14, Text = "What is the integral of 2x?", CategoryId = 1 },
            new { Id = 15, Text = "What is the value of sin(90°)?", CategoryId = 1 },
            new { Id = 16, Text = "What is the median of the numbers: 1, 3, 5, 7, 9?", CategoryId = 1 },
            new { Id = 17, Text = "What is 125 ÷ 5?", CategoryId = 1 },
            new { Id = 18, Text = "What is the volume of a cube with side length 3?", CategoryId = 1 },
            new { Id = 19, Text = "What is the GCD (Greatest Common Divisor) of 12 and 18?", CategoryId = 1 },
            new { Id = 20, Text = "What is (a + b)²?", CategoryId = 1 },
            new { Id = 21, Text = "What is the formula for the quadratic equation?", CategoryId = 1 },
            new { Id = 22, Text = "What is the value of cos(0°)?", CategoryId = 1 },
            new { Id = 23, Text = "What is 0.25 as a fraction?", CategoryId = 1 },
            new { Id = 24, Text = "What is the circumference of a circle with diameter 10?", CategoryId = 1 },
            new { Id = 25, Text = "What is the LCM (Least Common Multiple) of 4 and 6?", CategoryId = 1 },
            new { Id = 26, Text = "What is the standard form of a linear equation?", CategoryId = 1 },
            new { Id = 27, Text = "What is the value of e (Euler's number) rounded to 2 decimal places?", CategoryId = 1 },
            new { Id = 28, Text = "What is the sum of the first 10 positive integers?", CategoryId = 1 },
            new { Id = 29, Text = "What is the inverse of multiplication?", CategoryId = 1 },
            new { Id = 30, Text = "What is the value of tan(45°)?", CategoryId = 1 },

            // Computer Science Questions (31-60)
            new { Id = 31, Text = "What does CPU stand for?", CategoryId = 2 },
            new { Id = 32, Text = "What is the binary representation of the decimal number 8?", CategoryId = 2 },
            new { Id = 33, Text = "Which programming language is known as the 'mother of all languages'?", CategoryId = 2 },
            new { Id = 34, Text = "What does HTML stand for?", CategoryId = 2 },
            new { Id = 35, Text = "What is the time complexity of binary search?", CategoryId = 2 },
            new { Id = 36, Text = "What does SQL stand for?", CategoryId = 2 },
            new { Id = 37, Text = "Which data structure follows LIFO (Last In, First Out) principle?", CategoryId = 2 },
            new { Id = 38, Text = "What does OOP stand for?", CategoryId = 2 },
            new { Id = 39, Text = "What is the default port number for HTTP?", CategoryId = 2 },
            new { Id = 40, Text = "Which sorting algorithm has the best average-case time complexity?", CategoryId = 2 },
            new { Id = 41, Text = "What does API stand for?", CategoryId = 2 },
            new { Id = 42, Text = "What is the maximum value of a byte?", CategoryId = 2 },
            new { Id = 43, Text = "Which company developed the Java programming language?", CategoryId = 2 },
            new { Id = 44, Text = "What does TCP stand for?", CategoryId = 2 },
            new { Id = 45, Text = "What is the purpose of a compiler?", CategoryId = 2 },
            new { Id = 46, Text = "Which data structure is used for BFS (Breadth-First Search)?", CategoryId = 2 },
            new { Id = 47, Text = "What does IDE stand for?", CategoryId = 2 },
            new { Id = 48, Text = "What is the base of the hexadecimal number system?", CategoryId = 2 },
            new { Id = 49, Text = "Which protocol is used for secure web communication?", CategoryId = 2 },
            new { Id = 50, Text = "What does RAM stand for?", CategoryId = 2 },
            new { Id = 51, Text = "What is the worst-case time complexity of Quick Sort?", CategoryId = 2 },
            new { Id = 52, Text = "Which language is primarily used for web styling?", CategoryId = 2 },
            new { Id = 53, Text = "What does URL stand for?", CategoryId = 2 },
            new { Id = 54, Text = "What is the primary key in a database?", CategoryId = 2 },
            new { Id = 55, Text = "Which data structure is used for DFS (Depth-First Search)?", CategoryId = 2 },
            new { Id = 56, Text = "What does GUI stand for?", CategoryId = 2 },
            new { Id = 57, Text = "What is the time complexity of accessing an element in an array?", CategoryId = 2 },
            new { Id = 58, Text = "Which company created the C++ programming language?", CategoryId = 2 },
            new { Id = 59, Text = "What does DNS stand for?", CategoryId = 2 },
            new { Id = 60, Text = "What is the default port number for HTTPS?", CategoryId = 2 },

            // Chemistry Questions (61-90)
            new { Id = 61, Text = "What is the chemical symbol for Gold?", CategoryId = 3 },
            new { Id = 62, Text = "What is the atomic number of Carbon?", CategoryId = 3 },
            new { Id = 63, Text = "What is the most abundant gas in Earth's atmosphere?", CategoryId = 3 },
            new { Id = 64, Text = "What is the chemical formula for water?", CategoryId = 3 },
            new { Id = 65, Text = "What is the pH of pure water at 25°C?", CategoryId = 3 },
            new { Id = 66, Text = "What is the chemical symbol for Sodium?", CategoryId = 3 },
            new { Id = 67, Text = "How many electrons does a neutral Oxygen atom have?", CategoryId = 3 },
            new { Id = 68, Text = "What type of bond is formed when electrons are shared between atoms?", CategoryId = 3 },
            new { Id = 69, Text = "What is the chemical formula for methane?", CategoryId = 3 },
            new { Id = 70, Text = "What is the lightest element on the periodic table?", CategoryId = 3 },
            new { Id = 71, Text = "What is the chemical symbol for Iron?", CategoryId = 3 },
            new { Id = 72, Text = "What is the formula for calculating molarity?", CategoryId = 3 },
            new { Id = 73, Text = "What type of reaction occurs when an acid reacts with a base?", CategoryId = 3 },
            new { Id = 74, Text = "What is the atomic mass unit abbreviated as?", CategoryId = 3 },
            new { Id = 75, Text = "What is the chemical formula for table salt?", CategoryId = 3 },
            new { Id = 76, Text = "Which gas is produced when metals react with acids?", CategoryId = 3 },
            new { Id = 77, Text = "What is the chemical symbol for Calcium?", CategoryId = 3 },
            new { Id = 78, Text = "What is the process called when a liquid turns into a gas?", CategoryId = 3 },
            new { Id = 79, Text = "How many valence electrons does Carbon have?", CategoryId = 3 },
            new { Id = 80, Text = "What is the chemical formula for carbon dioxide?", CategoryId = 3 },
            new { Id = 81, Text = "What is the most electronegative element?", CategoryId = 3 },
            new { Id = 82, Text = "What is the chemical symbol for Silver?", CategoryId = 3 },
            new { Id = 83, Text = "What is the name of the horizontal rows in the periodic table?", CategoryId = 3 },
            new { Id = 84, Text = "What type of reaction involves the loss of electrons?", CategoryId = 3 },
            new { Id = 85, Text = "What is the chemical formula for ammonia?", CategoryId = 3 },
            new { Id = 86, Text = "What is the atomic number of Hydrogen?", CategoryId = 3 },
            new { Id = 87, Text = "What is the chemical symbol for Potassium?", CategoryId = 3 },
            new { Id = 88, Text = "What is the name of the vertical columns in the periodic table?", CategoryId = 3 },
            new { Id = 89, Text = "What is the chemical formula for sulfuric acid?", CategoryId = 3 },
            new { Id = 90, Text = "What is the process of splitting atoms called?", CategoryId = 3 }
        );

        // Answers
        modelBuilder.Entity<Answer>().HasData(
            // Mathematics Answers (Questions 1-30)
            // Question 1
            new { Id = 1, Text = "3.14", IsCorrect = true, QuestionId = 1 },
            new { Id = 2, Text = "3.16", IsCorrect = false, QuestionId = 1 },
            new { Id = 3, Text = "3.12", IsCorrect = false, QuestionId = 1 },
            new { Id = 4, Text = "3.18", IsCorrect = false, QuestionId = 1 },

            // Question 2
            new { Id = 5, Text = "12", IsCorrect = true, QuestionId = 2 },
            new { Id = 6, Text = "14", IsCorrect = false, QuestionId = 2 },
            new { Id = 7, Text = "10", IsCorrect = false, QuestionId = 2 },
            new { Id = 8, Text = "16", IsCorrect = false, QuestionId = 2 },

            // Question 3
            new { Id = 9, Text = "7", IsCorrect = true, QuestionId = 3 },
            new { Id = 10, Text = "5", IsCorrect = false, QuestionId = 3 },
            new { Id = 11, Text = "17", IsCorrect = false, QuestionId = 3 },
            new { Id = 12, Text = "12", IsCorrect = false, QuestionId = 3 },

            // Question 4
            new { Id = 13, Text = "30", IsCorrect = true, QuestionId = 4 },
            new { Id = 14, Text = "25", IsCorrect = false, QuestionId = 4 },
            new { Id = 15, Text = "35", IsCorrect = false, QuestionId = 4 },
            new { Id = 16, Text = "40", IsCorrect = false, QuestionId = 4 },

            // Question 5
            new { Id = 17, Text = "180°", IsCorrect = true, QuestionId = 5 },
            new { Id = 18, Text = "360°", IsCorrect = false, QuestionId = 5 },
            new { Id = 19, Text = "90°", IsCorrect = false, QuestionId = 5 },
            new { Id = 20, Text = "270°", IsCorrect = false, QuestionId = 5 },

            // Question 6
            new { Id = 21, Text = "16", IsCorrect = true, QuestionId = 6 },
            new { Id = 22, Text = "8", IsCorrect = false, QuestionId = 6 },
            new { Id = 23, Text = "12", IsCorrect = false, QuestionId = 6 },
            new { Id = 24, Text = "20", IsCorrect = false, QuestionId = 6 },

            // Question 7
            new { Id = 25, Text = "2x", IsCorrect = true, QuestionId = 7 },
            new { Id = 26, Text = "x²", IsCorrect = false, QuestionId = 7 },
            new { Id = 27, Text = "x", IsCorrect = false, QuestionId = 7 },
            new { Id = 28, Text = "2", IsCorrect = false, QuestionId = 7 },

            // Question 8
            new { Id = 29, Text = "25π", IsCorrect = true, QuestionId = 8 },
            new { Id = 30, Text = "10π", IsCorrect = false, QuestionId = 8 },
            new { Id = 31, Text = "5π", IsCorrect = false, QuestionId = 8 },
            new { Id = 32, Text = "50π", IsCorrect = false, QuestionId = 8 },

            // Question 9
            new { Id = 33, Text = "2", IsCorrect = true, QuestionId = 9 },
            new { Id = 34, Text = "10", IsCorrect = false, QuestionId = 9 },
            new { Id = 35, Text = "100", IsCorrect = false, QuestionId = 9 },
            new { Id = 36, Text = "1", IsCorrect = false, QuestionId = 9 },

            // Question 10
            new { Id = 37, Text = "3", IsCorrect = true, QuestionId = 10 },
            new { Id = 38, Text = "2", IsCorrect = false, QuestionId = 10 },
            new { Id = 39, Text = "1", IsCorrect = false, QuestionId = 10 },
            new { Id = 40, Text = "5", IsCorrect = false, QuestionId = 10 },

            // Question 11
            new { Id = 41, Text = "5040", IsCorrect = true, QuestionId = 11 },
            new { Id = 42, Text = "720", IsCorrect = false, QuestionId = 11 },
            new { Id = 43, Text = "49", IsCorrect = false, QuestionId = 11 },
            new { Id = 44, Text = "5000", IsCorrect = false, QuestionId = 11 },

            // Question 12
            new { Id = 45, Text = "32", IsCorrect = true, QuestionId = 12 },
            new { Id = 46, Text = "24", IsCorrect = false, QuestionId = 12 },
            new { Id = 47, Text = "20", IsCorrect = false, QuestionId = 12 },
            new { Id = 48, Text = "64", IsCorrect = false, QuestionId = 12 },

            // Question 13
            new { Id = 49, Text = "5", IsCorrect = true, QuestionId = 13 },
            new { Id = 50, Text = "7", IsCorrect = false, QuestionId = 13 },
            new { Id = 51, Text = "6", IsCorrect = false, QuestionId = 13 },
            new { Id = 52, Text = "25", IsCorrect = false, QuestionId = 13 },

            // Question 14
            new { Id = 53, Text = "x² + C", IsCorrect = true, QuestionId = 14 },
            new { Id = 54, Text = "2x + C", IsCorrect = false, QuestionId = 14 },
            new { Id = 55, Text = "x + C", IsCorrect = false, QuestionId = 14 },
            new { Id = 56, Text = "2x²", IsCorrect = false, QuestionId = 14 },

            // Question 15
            new { Id = 57, Text = "1", IsCorrect = true, QuestionId = 15 },
            new { Id = 58, Text = "0", IsCorrect = false, QuestionId = 15 },
            new { Id = 59, Text = "-1", IsCorrect = false, QuestionId = 15 },
            new { Id = 60, Text = "90", IsCorrect = false, QuestionId = 15 },

            // Question 16
            new { Id = 61, Text = "5", IsCorrect = true, QuestionId = 16 },
            new { Id = 62, Text = "4", IsCorrect = false, QuestionId = 16 },
            new { Id = 63, Text = "6", IsCorrect = false, QuestionId = 16 },
            new { Id = 64, Text = "25", IsCorrect = false, QuestionId = 16 },

            // Question 17
            new { Id = 65, Text = "25", IsCorrect = true, QuestionId = 17 },
            new { Id = 66, Text = "20", IsCorrect = false, QuestionId = 17 },
            new { Id = 67, Text = "30", IsCorrect = false, QuestionId = 17 },
            new { Id = 68, Text = "15", IsCorrect = false, QuestionId = 17 },

            // Question 18
            new { Id = 69, Text = "27", IsCorrect = true, QuestionId = 18 },
            new { Id = 70, Text = "9", IsCorrect = false, QuestionId = 18 },
            new { Id = 71, Text = "18", IsCorrect = false, QuestionId = 18 },
            new { Id = 72, Text = "81", IsCorrect = false, QuestionId = 18 },

            // Question 19
            new { Id = 73, Text = "6", IsCorrect = true, QuestionId = 19 },
            new { Id = 74, Text = "3", IsCorrect = false, QuestionId = 19 },
            new { Id = 75, Text = "9", IsCorrect = false, QuestionId = 19 },
            new { Id = 76, Text = "12", IsCorrect = false, QuestionId = 19 },

            // Question 20
            new { Id = 77, Text = "a² + 2ab + b²", IsCorrect = true, QuestionId = 20 },
            new { Id = 78, Text = "a² + b²", IsCorrect = false, QuestionId = 20 },
            new { Id = 79, Text = "a + b", IsCorrect = false, QuestionId = 20 },
            new { Id = 80, Text = "2a + 2b", IsCorrect = false, QuestionId = 20 },

            // Question 21
            new { Id = 81, Text = "x = (-b ± √(b²-4ac))/2a", IsCorrect = true, QuestionId = 21 },
            new { Id = 82, Text = "x = -b/2a", IsCorrect = false, QuestionId = 21 },
            new { Id = 83, Text = "x = b²-4ac", IsCorrect = false, QuestionId = 21 },
            new { Id = 84, Text = "x = a + b + c", IsCorrect = false, QuestionId = 21 },

            // Question 22
            new { Id = 85, Text = "1", IsCorrect = true, QuestionId = 22 },
            new { Id = 86, Text = "0", IsCorrect = false, QuestionId = 22 },
            new { Id = 87, Text = "-1", IsCorrect = false, QuestionId = 22 },
            new { Id = 88, Text = "√2/2", IsCorrect = false, QuestionId = 22 },

            // Question 23
            new { Id = 89, Text = "1/4", IsCorrect = true, QuestionId = 23 },
            new { Id = 90, Text = "1/2", IsCorrect = false, QuestionId = 23 },
            new { Id = 91, Text = "2/5", IsCorrect = false, QuestionId = 23 },
            new { Id = 92, Text = "1/3", IsCorrect = false, QuestionId = 23 },

            // Question 24
            new { Id = 93, Text = "10π", IsCorrect = true, QuestionId = 24 },
            new { Id = 94, Text = "20π", IsCorrect = false, QuestionId = 24 },
            new { Id = 95, Text = "5π", IsCorrect = false, QuestionId = 24 },
            new { Id = 96, Text = "25π", IsCorrect = false, QuestionId = 24 },

            // Question 25
            new { Id = 97, Text = "12", IsCorrect = true, QuestionId = 25 },
            new { Id = 98, Text = "8", IsCorrect = false, QuestionId = 25 },
            new { Id = 99, Text = "24", IsCorrect = false, QuestionId = 25 },
            new { Id = 100, Text = "6", IsCorrect = false, QuestionId = 25 },

            // Question 26
            new { Id = 101, Text = "Ax + By = C", IsCorrect = true, QuestionId = 26 },
            new { Id = 102, Text = "y = mx + b", IsCorrect = false, QuestionId = 26 },
            new { Id = 103, Text = "ax² + bx + c = 0", IsCorrect = false, QuestionId = 26 },
            new { Id = 104, Text = "x + y = 1", IsCorrect = false, QuestionId = 26 },

            // Question 27
            new { Id = 105, Text = "2.72", IsCorrect = true, QuestionId = 27 },
            new { Id = 106, Text = "2.54", IsCorrect = false, QuestionId = 27 },
            new { Id = 107, Text = "2.83", IsCorrect = false, QuestionId = 27 },
            new { Id = 108, Text = "3.14", IsCorrect = false, QuestionId = 27 },

            // Question 28
            new { Id = 109, Text = "55", IsCorrect = true, QuestionId = 28 },
            new { Id = 110, Text = "45", IsCorrect = false, QuestionId = 28 },
            new { Id = 111, Text = "100", IsCorrect = false, QuestionId = 28 },
            new { Id = 112, Text = "50", IsCorrect = false, QuestionId = 28 },

            // Question 29
            new { Id = 113, Text = "Division", IsCorrect = true, QuestionId = 29 },
            new { Id = 114, Text = "Addition", IsCorrect = false, QuestionId = 29 },
            new { Id = 115, Text = "Subtraction", IsCorrect = false, QuestionId = 29 },
            new { Id = 116, Text = "Exponentiation", IsCorrect = false, QuestionId = 29 },

            // Question 30
            new { Id = 117, Text = "1", IsCorrect = true, QuestionId = 30 },
            new { Id = 118, Text = "0", IsCorrect = false, QuestionId = 30 },
            new { Id = 119, Text = "√3", IsCorrect = false, QuestionId = 30 },
            new { Id = 120, Text = "√2/2", IsCorrect = false, QuestionId = 30 },

            // Computer Science Answers (Questions 31-60)
            // Question 31
            new { Id = 121, Text = "Central Processing Unit", IsCorrect = true, QuestionId = 31 },
            new { Id = 122, Text = "Computer Personal Unit", IsCorrect = false, QuestionId = 31 },
            new { Id = 123, Text = "Central Program Unit", IsCorrect = false, QuestionId = 31 },
            new { Id = 124, Text = "Computer Processing Unit", IsCorrect = false, QuestionId = 31 },

            // Question 32
            new { Id = 125, Text = "1000", IsCorrect = true, QuestionId = 32 },
            new { Id = 126, Text = "1001", IsCorrect = false, QuestionId = 32 },
            new { Id = 127, Text = "1010", IsCorrect = false, QuestionId = 32 },
            new { Id = 128, Text = "1100", IsCorrect = false, QuestionId = 32 },

            // Question 33
            new { Id = 129, Text = "C", IsCorrect = true, QuestionId = 33 },
            new { Id = 130, Text = "Assembly", IsCorrect = false, QuestionId = 33 },
            new { Id = 131, Text = "FORTRAN", IsCorrect = false, QuestionId = 33 },
            new { Id = 132, Text = "COBOL", IsCorrect = false, QuestionId = 33 },

            // Question 34
            new { Id = 133, Text = "HyperText Markup Language", IsCorrect = true, QuestionId = 34 },
            new { Id = 134, Text = "Home Tool Markup Language", IsCorrect = false, QuestionId = 34 },
            new { Id = 135, Text = "Hyperlinks and Text Markup Language", IsCorrect = false, QuestionId = 34 },
            new { Id = 136, Text = "HyperText Management Language", IsCorrect = false, QuestionId = 34 },

            // Question 35
            new { Id = 137, Text = "O(log n)", IsCorrect = true, QuestionId = 35 },
            new { Id = 138, Text = "O(n)", IsCorrect = false, QuestionId = 35 },
            new { Id = 139, Text = "O(n²)", IsCorrect = false, QuestionId = 35 },
            new { Id = 140, Text = "O(1)", IsCorrect = false, QuestionId = 35 },

            // Question 36
            new { Id = 141, Text = "Structured Query Language", IsCorrect = true, QuestionId = 36 },
            new { Id = 142, Text = "Standard Query Language", IsCorrect = false, QuestionId = 36 },
            new { Id = 143, Text = "Sequential Query Language", IsCorrect = false, QuestionId = 36 },
            new { Id = 144, Text = "Simple Query Language", IsCorrect = false, QuestionId = 36 },

            // Question 37
            new { Id = 145, Text = "Stack", IsCorrect = true, QuestionId = 37 },
            new { Id = 146, Text = "Queue", IsCorrect = false, QuestionId = 37 },
            new { Id = 147, Text = "Array", IsCorrect = false, QuestionId = 37 },
            new { Id = 148, Text = "Linked List", IsCorrect = false, QuestionId = 37 },

            // Question 38
            new { Id = 149, Text = "Object-Oriented Programming", IsCorrect = true, QuestionId = 38 },
            new { Id = 150, Text = "Object-Oriented Process", IsCorrect = false, QuestionId = 38 },
            new { Id = 151, Text = "Oriented Object Programming", IsCorrect = false, QuestionId = 38 },
            new { Id = 152, Text = "Object Oriented Procedure", IsCorrect = false, QuestionId = 38 },

            // Question 39
            new { Id = 153, Text = "80", IsCorrect = true, QuestionId = 39 },
            new { Id = 154, Text = "443", IsCorrect = false, QuestionId = 39 },
            new { Id = 155, Text = "8080", IsCorrect = false, QuestionId = 39 },
            new { Id = 156, Text = "21", IsCorrect = false, QuestionId = 39 },

            // Question 40
            new { Id = 157, Text = "Merge Sort", IsCorrect = true, QuestionId = 40 },
            new { Id = 158, Text = "Bubble Sort", IsCorrect = false, QuestionId = 40 },
            new { Id = 159, Text = "Selection Sort", IsCorrect = false, QuestionId = 40 },
            new { Id = 160, Text = "Insertion Sort", IsCorrect = false, QuestionId = 40 },

            // Question 41
            new { Id = 161, Text = "Application Programming Interface", IsCorrect = true, QuestionId = 41 },
            new { Id = 162, Text = "Application Program Interface", IsCorrect = false, QuestionId = 41 },
            new { Id = 163, Text = "Applied Programming Interface", IsCorrect = false, QuestionId = 41 },
            new { Id = 164, Text = "Application Process Interface", IsCorrect = false, QuestionId = 41 },

            // Question 42
            new { Id = 165, Text = "255", IsCorrect = true, QuestionId = 42 },
            new { Id = 166, Text = "256", IsCorrect = false, QuestionId = 42 },
            new { Id = 167, Text = "128", IsCorrect = false, QuestionId = 42 },
            new { Id = 168, Text = "127", IsCorrect = false, QuestionId = 42 },

            // Question 43
            new { Id = 169, Text = "Sun Microsystems", IsCorrect = true, QuestionId = 43 },
            new { Id = 170, Text = "Oracle", IsCorrect = false, QuestionId = 43 },
            new { Id = 171, Text = "Microsoft", IsCorrect = false, QuestionId = 43 },
            new { Id = 172, Text = "IBM", IsCorrect = false, QuestionId = 43 },

            // Question 44
            new { Id = 173, Text = "Transmission Control Protocol", IsCorrect = true, QuestionId = 44 },
            new { Id = 174, Text = "Transfer Control Protocol", IsCorrect = false, QuestionId = 44 },
            new { Id = 175, Text = "Transport Control Protocol", IsCorrect = false, QuestionId = 44 },
            new { Id = 176, Text = "Terminal Control Protocol", IsCorrect = false, QuestionId = 44 },

            // Question 45
            new { Id = 177, Text = "Convert source code to machine code", IsCorrect = true, QuestionId = 45 },
            new { Id = 178, Text = "Execute programs", IsCorrect = false, QuestionId = 45 },
            new { Id = 179, Text = "Debug programs", IsCorrect = false, QuestionId = 45 },
            new { Id = 180, Text = "Edit source code", IsCorrect = false, QuestionId = 45 },

            // Question 46
            new { Id = 181, Text = "Queue", IsCorrect = true, QuestionId = 46 },
            new { Id = 182, Text = "Stack", IsCorrect = false, QuestionId = 46 },
            new { Id = 183, Text = "Array", IsCorrect = false, QuestionId = 46 },
            new { Id = 184, Text = "Tree", IsCorrect = false, QuestionId = 46 },

            // Question 47
            new { Id = 185, Text = "Integrated Development Environment", IsCorrect = true, QuestionId = 47 },
            new { Id = 186, Text = "Interactive Development Environment", IsCorrect = false, QuestionId = 47 },
            new { Id = 187, Text = "Internal Development Environment", IsCorrect = false, QuestionId = 47 },
            new { Id = 188, Text = "Independent Development Environment", IsCorrect = false, QuestionId = 47 },

            // Question 48
            new { Id = 189, Text = "16", IsCorrect = true, QuestionId = 48 },
            new { Id = 190, Text = "10", IsCorrect = false, QuestionId = 48 },
            new { Id = 191, Text = "8", IsCorrect = false, QuestionId = 48 },
            new { Id = 192, Text = "2", IsCorrect = false, QuestionId = 48 },

            // Question 49
            new { Id = 193, Text = "HTTPS", IsCorrect = true, QuestionId = 49 },
            new { Id = 194, Text = "HTTP", IsCorrect = false, QuestionId = 49 },
            new { Id = 195, Text = "FTP", IsCorrect = false, QuestionId = 49 },
            new { Id = 196, Text = "SSH", IsCorrect = false, QuestionId = 49 },

            // Question 50
            new { Id = 197, Text = "Random Access Memory", IsCorrect = true, QuestionId = 50 },
            new { Id = 198, Text = "Read Access Memory", IsCorrect = false, QuestionId = 50 },
            new { Id = 199, Text = "Rapid Access Memory", IsCorrect = false, QuestionId = 50 },
            new { Id = 200, Text = "Remote Access Memory", IsCorrect = false, QuestionId = 50 },

            // Question 51
            new { Id = 201, Text = "O(n²)", IsCorrect = true, QuestionId = 51 },
            new { Id = 202, Text = "O(n log n)", IsCorrect = false, QuestionId = 51 },
            new { Id = 203, Text = "O(n)", IsCorrect = false, QuestionId = 51 },
            new { Id = 204, Text = "O(log n)", IsCorrect = false, QuestionId = 51 },

            // Question 52
            new { Id = 205, Text = "CSS", IsCorrect = true, QuestionId = 52 },
            new { Id = 206, Text = "HTML", IsCorrect = false, QuestionId = 52 },
            new { Id = 207, Text = "JavaScript", IsCorrect = false, QuestionId = 52 },
            new { Id = 208, Text = "PHP", IsCorrect = false, QuestionId = 52 },

            // Question 53
            new { Id = 209, Text = "Uniform Resource Locator", IsCorrect = true, QuestionId = 53 },
            new { Id = 210, Text = "Universal Resource Locator", IsCorrect = false, QuestionId = 53 },
            new { Id = 211, Text = "Unified Resource Locator", IsCorrect = false, QuestionId = 53 },
            new { Id = 212, Text = "Unique Resource Locator", IsCorrect = false, QuestionId = 53 },

            // Question 54
            new { Id = 213, Text = "A unique identifier for each record", IsCorrect = true, QuestionId = 54 },
            new { Id = 214, Text = "The first column in a table", IsCorrect = false, QuestionId = 54 },
            new { Id = 215, Text = "The most important data", IsCorrect = false, QuestionId = 54 },
            new { Id = 216, Text = "A password for the database", IsCorrect = false, QuestionId = 54 },

            // Question 55
            new { Id = 217, Text = "Stack", IsCorrect = true, QuestionId = 55 },
            new { Id = 218, Text = "Queue", IsCorrect = false, QuestionId = 55 },
            new { Id = 219, Text = "Array", IsCorrect = false, QuestionId = 55 },
            new { Id = 220, Text = "Linked List", IsCorrect = false, QuestionId = 55 },

            // Question 56
            new { Id = 221, Text = "Graphical User Interface", IsCorrect = true, QuestionId = 56 },
            new { Id = 222, Text = "General User Interface", IsCorrect = false, QuestionId = 56 },
            new { Id = 223, Text = "Global User Interface", IsCorrect = false, QuestionId = 56 },
            new { Id = 224, Text = "Generic User Interface", IsCorrect = false, QuestionId = 56 },

            // Question 57
            new { Id = 225, Text = "O(1)", IsCorrect = true, QuestionId = 57 },
            new { Id = 226, Text = "O(n)", IsCorrect = false, QuestionId = 57 },
            new { Id = 227, Text = "O(log n)", IsCorrect = false, QuestionId = 57 },
            new { Id = 228, Text = "O(n²)", IsCorrect = false, QuestionId = 57 },

            // Question 58
            new { Id = 229, Text = "Bell Labs", IsCorrect = true, QuestionId = 58 },
            new { Id = 230, Text = "Microsoft", IsCorrect = false, QuestionId = 58 },
            new { Id = 231, Text = "IBM", IsCorrect = false, QuestionId = 58 },
            new { Id = 232, Text = "Sun Microsystems", IsCorrect = false, QuestionId = 58 },

            // Question 59
            new { Id = 233, Text = "Domain Name System", IsCorrect = true, QuestionId = 59 },
            new { Id = 234, Text = "Domain Network System", IsCorrect = false, QuestionId = 59 },
            new { Id = 235, Text = "Digital Name System", IsCorrect = false, QuestionId = 59 },
            new { Id = 236, Text = "Data Network System", IsCorrect = false, QuestionId = 59 },

            // Question 60
            new { Id = 237, Text = "443", IsCorrect = true, QuestionId = 60 },
            new { Id = 238, Text = "80", IsCorrect = false, QuestionId = 60 },
            new { Id = 239, Text = "8080", IsCorrect = false, QuestionId = 60 },
            new { Id = 240, Text = "21", IsCorrect = false, QuestionId = 60 },

            // Chemistry Answers (Questions 61-90)
            // Question 61
            new { Id = 241, Text = "Au", IsCorrect = true, QuestionId = 61 },
            new { Id = 242, Text = "Go", IsCorrect = false, QuestionId = 61 },
            new { Id = 243, Text = "Gd", IsCorrect = false, QuestionId = 61 },
            new { Id = 244, Text = "Ag", IsCorrect = false, QuestionId = 61 },

            // Question 62
            new { Id = 245, Text = "6", IsCorrect = true, QuestionId = 62 },
            new { Id = 246, Text = "12", IsCorrect = false, QuestionId = 62 },
            new { Id = 247, Text = "8", IsCorrect = false, QuestionId = 62 },
            new { Id = 248, Text = "4", IsCorrect = false, QuestionId = 62 },

            // Question 63
            new { Id = 249, Text = "Nitrogen", IsCorrect = true, QuestionId = 63 },
            new { Id = 250, Text = "Oxygen", IsCorrect = false, QuestionId = 63 },
            new { Id = 251, Text = "Carbon Dioxide", IsCorrect = false, QuestionId = 63 },
            new { Id = 252, Text = "Argon", IsCorrect = false, QuestionId = 63 },

            // Question 64
            new { Id = 253, Text = "H₂O", IsCorrect = true, QuestionId = 64 },
            new { Id = 254, Text = "H₂O₂", IsCorrect = false, QuestionId = 64 },
            new { Id = 255, Text = "HO", IsCorrect = false, QuestionId = 64 },
            new { Id = 256, Text = "H₃O", IsCorrect = false, QuestionId = 64 },

            // Question 65
            new { Id = 257, Text = "7", IsCorrect = true, QuestionId = 65 },
            new { Id = 258, Text = "0", IsCorrect = false, QuestionId = 65 },
            new { Id = 259, Text = "14", IsCorrect = false, QuestionId = 65 },
            new { Id = 260, Text = "1", IsCorrect = false, QuestionId = 65 },

            // Question 66
            new { Id = 261, Text = "Na", IsCorrect = true, QuestionId = 66 },
            new { Id = 262, Text = "So", IsCorrect = false, QuestionId = 66 },
            new { Id = 263, Text = "S", IsCorrect = false, QuestionId = 66 },
            new { Id = 264, Text = "N", IsCorrect = false, QuestionId = 66 },

            // Question 67
            new { Id = 265, Text = "8", IsCorrect = true, QuestionId = 67 },
            new { Id = 266, Text = "6", IsCorrect = false, QuestionId = 67 },
            new { Id = 267, Text = "16", IsCorrect = false, QuestionId = 67 },
            new { Id = 268, Text = "2", IsCorrect = false, QuestionId = 67 },

            // Question 68
            new { Id = 269, Text = "Covalent bond", IsCorrect = true, QuestionId = 68 },
            new { Id = 270, Text = "Ionic bond", IsCorrect = false, QuestionId = 68 },
            new { Id = 271, Text = "Metallic bond", IsCorrect = false, QuestionId = 68 },
            new { Id = 272, Text = "Hydrogen bond", IsCorrect = false, QuestionId = 68 },

            // Question 69
            new { Id = 273, Text = "CH₄", IsCorrect = true, QuestionId = 69 },
            new { Id = 274, Text = "CH₃", IsCorrect = false, QuestionId = 69 },
            new { Id = 275, Text = "C₂H₆", IsCorrect = false, QuestionId = 69 },
            new { Id = 276, Text = "CH₂", IsCorrect = false, QuestionId = 69 },

            // Question 70
            new { Id = 277, Text = "Hydrogen", IsCorrect = true, QuestionId = 70 },
            new { Id = 278, Text = "Helium", IsCorrect = false, QuestionId = 70 },
            new { Id = 279, Text = "Lithium", IsCorrect = false, QuestionId = 70 },
            new { Id = 280, Text = "Carbon", IsCorrect = false, QuestionId = 70 },

            // Question 71
            new { Id = 281, Text = "Fe", IsCorrect = true, QuestionId = 71 },
            new { Id = 282, Text = "Ir", IsCorrect = false, QuestionId = 71 },
            new { Id = 283, Text = "In", IsCorrect = false, QuestionId = 71 },
            new { Id = 284, Text = "I", IsCorrect = false, QuestionId = 71 },

            // Question 72
            new { Id = 285, Text = "M = n/V", IsCorrect = true, QuestionId = 72 },
            new { Id = 286, Text = "M = n × V", IsCorrect = false, QuestionId = 72 },
            new { Id = 287, Text = "M = V/n", IsCorrect = false, QuestionId = 72 },
            new { Id = 288, Text = "M = n + V", IsCorrect = false, QuestionId = 72 },

            // Question 73
            new { Id = 289, Text = "Neutralization", IsCorrect = true, QuestionId = 73 },
            new { Id = 290, Text = "Oxidation", IsCorrect = false, QuestionId = 73 },
            new { Id = 291, Text = "Reduction", IsCorrect = false, QuestionId = 73 },
            new { Id = 292, Text = "Combustion", IsCorrect = false, QuestionId = 73 },

            // Question 74
            new { Id = 293, Text = "amu", IsCorrect = true, QuestionId = 74 },
            new { Id = 294, Text = "mol", IsCorrect = false, QuestionId = 74 },
            new { Id = 295, Text = "g", IsCorrect = false, QuestionId = 74 },
            new { Id = 296, Text = "kg", IsCorrect = false, QuestionId = 74 },

            // Question 75
            new { Id = 297, Text = "NaCl", IsCorrect = true, QuestionId = 75 },
            new { Id = 298, Text = "KCl", IsCorrect = false, QuestionId = 75 },
            new { Id = 299, Text = "CaCl₂", IsCorrect = false, QuestionId = 75 },
            new { Id = 300, Text = "MgCl₂", IsCorrect = false, QuestionId = 75 },

            // Question 76
            new { Id = 301, Text = "Hydrogen", IsCorrect = true, QuestionId = 76 },
            new { Id = 302, Text = "Oxygen", IsCorrect = false, QuestionId = 76 },
            new { Id = 303, Text = "Carbon dioxide", IsCorrect = false, QuestionId = 76 },
            new { Id = 304, Text = "Nitrogen", IsCorrect = false, QuestionId = 76 },

            // Question 77
            new { Id = 305, Text = "Ca", IsCorrect = true, QuestionId = 77 },
            new { Id = 306, Text = "C", IsCorrect = false, QuestionId = 77 },
            new { Id = 307, Text = "Cl", IsCorrect = false, QuestionId = 77 },
            new { Id = 308, Text = "Cr", IsCorrect = false, QuestionId = 77 },

            // Question 78
            new { Id = 309, Text = "Vaporization", IsCorrect = true, QuestionId = 78 },
            new { Id = 310, Text = "Condensation", IsCorrect = false, QuestionId = 78 },
            new { Id = 311, Text = "Sublimation", IsCorrect = false, QuestionId = 78 },
            new { Id = 312, Text = "Freezing", IsCorrect = false, QuestionId = 78 },

            // Question 79
            new { Id = 313, Text = "4", IsCorrect = true, QuestionId = 79 },
            new { Id = 314, Text = "6", IsCorrect = false, QuestionId = 79 },
            new { Id = 315, Text = "2", IsCorrect = false, QuestionId = 79 },
            new { Id = 316, Text = "8", IsCorrect = false, QuestionId = 79 },

            // Question 80
            new { Id = 317, Text = "CO₂", IsCorrect = true, QuestionId = 80 },
            new { Id = 318, Text = "CO", IsCorrect = false, QuestionId = 80 },
            new { Id = 319, Text = "C₂O", IsCorrect = false, QuestionId = 80 },
            new { Id = 320, Text = "C₂O₂", IsCorrect = false, QuestionId = 80 },

            // Question 81
            new { Id = 321, Text = "Fluorine", IsCorrect = true, QuestionId = 81 },
            new { Id = 322, Text = "Oxygen", IsCorrect = false, QuestionId = 81 },
            new { Id = 323, Text = "Nitrogen", IsCorrect = false, QuestionId = 81 },
            new { Id = 324, Text = "Chlorine", IsCorrect = false, QuestionId = 81 },

            // Question 82
            new { Id = 325, Text = "Ag", IsCorrect = true, QuestionId = 82 },
            new { Id = 326, Text = "Si", IsCorrect = false, QuestionId = 82 },
            new { Id = 327, Text = "S", IsCorrect = false, QuestionId = 82 },
            new { Id = 328, Text = "Sv", IsCorrect = false, QuestionId = 82 },

            // Question 83
            new { Id = 329, Text = "Periods", IsCorrect = true, QuestionId = 83 },
            new { Id = 330, Text = "Groups", IsCorrect = false, QuestionId = 83 },
            new { Id = 331, Text = "Families", IsCorrect = false, QuestionId = 83 },
            new { Id = 332, Text = "Series", IsCorrect = false, QuestionId = 83 },

            // Question 84
            new { Id = 333, Text = "Oxidation", IsCorrect = true, QuestionId = 84 },
            new { Id = 334, Text = "Reduction", IsCorrect = false, QuestionId = 84 },
            new { Id = 335, Text = "Neutralization", IsCorrect = false, QuestionId = 84 },
            new { Id = 336, Text = "Hydrolysis", IsCorrect = false, QuestionId = 84 },

            // Question 85
            new { Id = 337, Text = "NH₃", IsCorrect = true, QuestionId = 85 },
            new { Id = 338, Text = "NH₄", IsCorrect = false, QuestionId = 85 },
            new { Id = 339, Text = "N₂H₄", IsCorrect = false, QuestionId = 85 },
            new { Id = 340, Text = "NH₂", IsCorrect = false, QuestionId = 85 },

            // Question 86
            new { Id = 341, Text = "1", IsCorrect = true, QuestionId = 86 },
            new { Id = 342, Text = "2", IsCorrect = false, QuestionId = 86 },
            new { Id = 343, Text = "0", IsCorrect = false, QuestionId = 86 },
            new { Id = 344, Text = "3", IsCorrect = false, QuestionId = 86 },

            // Question 87
            new { Id = 345, Text = "K", IsCorrect = true, QuestionId = 87 },
            new { Id = 346, Text = "P", IsCorrect = false, QuestionId = 87 },
            new { Id = 347, Text = "Po", IsCorrect = false, QuestionId = 87 },
            new { Id = 348, Text = "Pt", IsCorrect = false, QuestionId = 87 },

            // Question 88
            new { Id = 349, Text = "Groups", IsCorrect = true, QuestionId = 88 },
            new { Id = 350, Text = "Periods", IsCorrect = false, QuestionId = 88 },
            new { Id = 351, Text = "Families", IsCorrect = false, QuestionId = 88 },
            new { Id = 352, Text = "Series", IsCorrect = false, QuestionId = 88 },

            // Question 89
            new { Id = 353, Text = "H₂SO₄", IsCorrect = true, QuestionId = 89 },
            new { Id = 354, Text = "HCl", IsCorrect = false, QuestionId = 89 },
            new { Id = 355, Text = "HNO₃", IsCorrect = false, QuestionId = 89 },
            new { Id = 356, Text = "H₃PO₄", IsCorrect = false, QuestionId = 89 },

            // Question 90
            new { Id = 357, Text = "Nuclear fission", IsCorrect = true, QuestionId = 90 },
            new { Id = 358, Text = "Nuclear fusion", IsCorrect = false, QuestionId = 90 },
            new { Id = 359, Text = "Radioactive decay", IsCorrect = false, QuestionId = 90 },
            new { Id = 360, Text = "Nuclear reaction", IsCorrect = false, QuestionId = 90 }
        );
    }
}
