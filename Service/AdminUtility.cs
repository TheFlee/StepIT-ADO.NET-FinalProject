using StepIT_ADO.NET_FinalProject.Classes;
using StepIT_ADO.NET_FinalProjectl;

namespace StepIT_ADO.NET_FinalProject.Service;

internal class AdminUtility
{
    private readonly QuizContext _context;

    public AdminUtility(QuizContext context)
    {
        _context = context;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===== Admin Utility =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Add Question");
            Console.WriteLine("0. Switch to User Panel");
            Console.ResetColor();

            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    AddQuestion();
                    break;
                case "0":
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

    private void AddCategory()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Enter category name: ");
        Console.ResetColor();
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Category name cannot be empty.");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }

        _context.Categories.Add(new Category { Name = name });
        _context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Category '{name}' added successfully!");
        Console.ResetColor();
        Console.ReadKey();
    }

    private void AddQuestion()
    {
        Console.Clear();
        var categories = _context.Categories.ToList();

        if (!categories.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No categories found. Add a category first.");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Available Categories:");
        Console.ResetColor();

        foreach (var c in categories)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{c.Id}. {c.Name}");
            Console.ResetColor();
        }

        int categoryId;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Select category id: ");
            Console.ResetColor();
            string? input = Console.ReadLine();
            if (int.TryParse(input, out categoryId) && categories.Any(c => c.Id == categoryId))
                break;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid category id.");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Enter question text: ");
        Console.ResetColor();
        string questionText = Console.ReadLine() ?? "";

        var question = new Question
        {
            Text = questionText,
            CategoryId = categoryId,
            Answers = new List<Answer>()
        };

        for (int i = 1; i <= 4; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Enter answer {i}: ");
            Console.ResetColor();
            string answerText = Console.ReadLine() ?? "";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Is this answer correct? (y/n): ");
            Console.ResetColor();
            string correctInput = Console.ReadLine()?.ToLower() ?? "n";
            bool isCorrect = correctInput == "y";

            question.Answers.Add(new Answer { Text = answerText, IsCorrect = isCorrect });
        }

        _context.Questions.Add(question);
        _context.SaveChanges();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Question added successfully!");
        Console.ResetColor();
        Console.ReadKey();
    }
}
