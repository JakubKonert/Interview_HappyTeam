using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Config;

namespace Interview_HappyTeam_Backend.Core.Core
{
    public static class Setter
    {
        public static void Set(AppDbContext context)
        {
            ErrorLogger.ErrorLogger.SetDataBase(context);
            CurrentConfig.SetDataBase(context);
            CurrentConfig.GetCurrentConfig();
        }
    }
}
