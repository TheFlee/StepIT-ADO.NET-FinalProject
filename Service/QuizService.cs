using Microsoft.EntityFrameworkCore;
using StepIT_ADO.NET_FinalProject.Classes;
using StepIT_ADO.NET_FinalProjectl;

namespace StepIT_ADO.NET_FinalProject.Service;

internal class QuizService
{
    private readonly QuizContext _context;

    public QuizService(QuizContext context)
    {
        _context = context;
    }

    public List<Question> StartQuiz(int? categoryId = null)
    {
        var questions = _context.Questions
        .Include(q => q.Answers)
        .ToList();

        if (categoryId is not null)
            questions = questions.Where(q => q.CategoryId == categoryId.Value).ToList();

        return questions
            .OrderBy(q => Guid.NewGuid()) // Randomize etmek uchun (SQL-de RAND())
            .Take(20)
            .ToList();
    }

    public void SaveResult(int userId, int? categoryId, int score)
    {
        int attempt = _context.Results
        .Count(r => r.UserId == userId && r.CategoryId == categoryId) + 1;

        var result = new Result
        {
            UserId = userId,
            CategoryId = categoryId,
            Score = score,
            Attempt = attempt
        };

        _context.Results.Add(result);
        _context.SaveChanges();
    }

    public List<Result> GetUserResults(int userId)
    {
        return _context.Results
            .Include(r => r.Category)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.Score)
            .ToList();
    }

    public List<Result> GetTopResults(int? categoryId)
    {
        return _context.Results
            .Include(r => r.User)
            .Where(r => categoryId.HasValue ? r.CategoryId == categoryId.Value : r.CategoryId == null)
            .OrderByDescending(r => r.Score)
            .Take(20)
            .ToList();
    }
}
