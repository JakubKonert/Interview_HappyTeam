using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.ErrorLogger
{
    public static class ErrorLogger
    {
        private static AppDbContext _context { get; set; }

        public static void SetDataBase(AppDbContext context)
        { 
            _context = context;
        }

        public static void LogError(string errorName, string errorMessage) 
        {
            ErrorLog error = new ErrorLog()
            {
                ErrorName = errorName,
                ErrorMessage = errorMessage
            };
            LogError(error);
        }

        private static void LogError(ErrorLog error)
        {
            _context.ErrorLogs.Add(error);
            _context.SaveChangesAsync();
        }
    }
}
