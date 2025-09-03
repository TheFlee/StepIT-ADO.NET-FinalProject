using Microsoft.Extensions.Configuration;
using StepIT_ADO.NET_FinalProject.Classes;
using StepIT_ADO.NET_FinalProject.Service;
using StepIT_ADO.NET_FinalProjectl;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var config = builder.Build();
string connectionString = config.GetConnectionString("MyJCS")!;

using var db = new QuizContext(connectionString);

var userService = new UserService(db);
var quizService = new QuizService(db);
var adminUtility = new AdminUtility(db);

User? currentUser = null;

while (true)
{
    if (currentUser == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("===== User Login =====");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Switch to Admin Panel");
        Console.WriteLine("0. Exit");
        Console.Write("Choose: ");
        Console.ResetColor();
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Username: ");
                Console.ResetColor();
                string username = Console.ReadLine()!;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Password: ");
                Console.ResetColor();
                string password = Console.ReadLine()!;

                DateTime birthdate;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Birthdate (dd-mm-yyyy): ");
                    Console.ResetColor();
                    string? bdInput = Console.ReadLine();
                    if (DateTime.TryParse(bdInput, out birthdate))
                        break;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date. Try again.");
                    Console.ResetColor();
                }

                if (userService.Register(username, password, birthdate))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Registration successful!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Username already exists or invalid input!");
                    Console.ResetColor();
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "2":
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Username: ");
                Console.ResetColor();
                username = Console.ReadLine()!;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Password: ");
                Console.ResetColor();
                password = Console.ReadLine()!;

                currentUser = userService.Login(username, password);
                if (currentUser != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Welcome, {currentUser.Username}!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid credentials!");
                    Console.ResetColor();
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            case "3":
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===== Admin Login =====");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Username: ");
                Console.ResetColor();
                string adminUser = Console.ReadLine() ?? "";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Password: ");
                Console.ResetColor();
                string adminPass = Console.ReadLine() ?? "";

                if (adminUser == "admin" && adminPass == "admin")
                {
                    adminUtility.ShowMenu();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid admin credentials!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                break;

            case "0":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Goodbye!");
                Console.ResetColor();
                return;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice! Press any key to try again...");
                Console.ResetColor();
                Console.ReadKey();
                break;
        }
    }
    else
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"===== Menu (Logged in as {currentUser.Username}) =====");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("1. Start Quiz");
        Console.WriteLine("2. View My Results");
        Console.WriteLine("3. View Leaderboard");
        Console.WriteLine("4. Update Account");
        Console.WriteLine("5. Logout");
        Console.WriteLine("0. Exit");
        Console.Write("Choose: ");
        Console.ResetColor();
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();

                var categories = db.Categories.ToList();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Available Categories:");
                Console.ResetColor();
                foreach (var c in categories)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{c.Id}. {c.Name}");
                    Console.ResetColor();
                }

                int? categoryId = null;

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Choose number (or leave empty for mixed): ");
                    Console.ResetColor();
                    string? catInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(catInput))
                    {
                        categoryId = null;
                        break;
                    }

                    if (int.TryParse(catInput, out int catIdParsed) && categories.Any(c => c.Id == catIdParsed))
                    {
                        categoryId = catIdParsed;
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Enter a valid number or leave empty.");
                    Console.ResetColor();
                }

                var questions = quizService.StartQuiz(categoryId);
                int score = 0;

                foreach (var q in questions.ToList())
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\n{q.Text}");
                    Console.ResetColor();

                    var answers = q.Answers.ToList();
                    for (int i = 0; i < answers.Count; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{i + 1}. {answers[i].Text}");
                        Console.ResetColor();
                    }

                    int ansIndex;
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Your answer (number): ");
                        Console.ResetColor();
                        string? ansInput = Console.ReadLine();

                        if (int.TryParse(ansInput, out ansIndex) && ansIndex >= 1 && ansIndex <= answers.Count)
                        {
                            ansIndex -= 1;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid choice. Enter a number between 1 and {answers.Count}.");
                        Console.ResetColor();
                    }

                    if (answers[ansIndex].IsCorrect)
                        score++;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nQuiz finished! Score: {score}/{questions.Count}");
                Console.ResetColor();

                quizService.SaveResult(currentUser.Id, categoryId, score);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "2":
                Console.Clear();
                var results = quizService.GetUserResults(currentUser.Id);
                foreach (var r in results)
                {
                    string categoryName = r.Category?.Name ?? "Mixed";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Attempt #{r.Attempt} | Category: {categoryName}, Score: {r.Score}");
                    Console.ResetColor();
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "3":
                Console.Clear();

                categories = db.Categories.ToList();

                // Add a "Mixed" option
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Available Categories:");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("0. Mixed");
                Console.ResetColor();

                foreach (var c in categories)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{c.Id}. {c.Name}");
                    Console.ResetColor();
                }

                int categoryIdLb;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Select number: ");
                    Console.ResetColor();
                    string? lbInput = Console.ReadLine();

                    if (int.TryParse(lbInput, out categoryIdLb) &&
                        (categoryIdLb == 0 || categories.Any(c => c.Id == categoryIdLb)))
                        break;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Enter a valid number.");
                    Console.ResetColor();
                }

                int? selectedCategoryId = categoryIdLb == 0 ? null : categoryIdLb;

                var leaderboard = quizService.GetTopResults(selectedCategoryId);

                if (!leaderboard.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No results for this category yet.");
                    Console.ResetColor();
                }
                else
                {
                    string categoryName = categoryIdLb == 0 ? "Mixed" : categories.First(c => c.Id == categoryIdLb).Name;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n=== Top 20 for {categoryName} ===");
                    Console.ResetColor();

                    int rank = 1;
                    foreach (var r in leaderboard)
                    {
                        string usernameLb = r.User?.Username ?? "Unknown";
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{rank}. {usernameLb} - {r.Score}");
                        Console.ResetColor();
                        rank++;
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "4":
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("New password (leave empty to skip): ");
                Console.ResetColor();
                string newPass = Console.ReadLine()!;

                DateTime? bd = null;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("New birthdate (leave empty to skip): ");
                    Console.ResetColor();
                    string newDate = Console.ReadLine()!;
                    if (string.IsNullOrWhiteSpace(newDate))
                        break;
                    if (DateTime.TryParse(newDate, out DateTime parsed))
                    {
                        bd = parsed;
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format.");
                    Console.ResetColor();
                }

                userService.UpdateUser(currentUser.Id, string.IsNullOrWhiteSpace(newPass) ? null : newPass, bd);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Account updated.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "5":
                Console.Clear();
                currentUser = null;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Logged out.");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "0":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Goodbye!");
                Console.ResetColor();
                return;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice! Press any key to try again...");
                Console.ResetColor();
                Console.ReadKey();
                break;
        }
    }
}
