using StepIT_ADO.NET_FinalProject.Classes;
using StepIT_ADO.NET_FinalProjectl;
using Microsoft.EntityFrameworkCore;

namespace StepIT_ADO.NET_FinalProject.Service;

internal class ResultService
{
    private readonly QuizContext _context;

    public ResultService(QuizContext context)
    {
        _context = context;
    }

    public List<Result> GetUserResults(int userId)
    {
        return _context.Results
            .Include(r => r.Category)
            .Where(r => r.UserId == userId)
            .ToList();
    }

    public List<Result> GetTop20(int categoryId)
    {
        return _context.Results
            .Include(r => r.User)
            .Where(r => r.CategoryId == categoryId)
            .OrderByDescending(r => r.Score)
            .Take(20)
            .ToList();
    }
}
