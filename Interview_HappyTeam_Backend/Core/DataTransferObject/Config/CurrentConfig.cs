using Interview_HappyTeam_Backend.Core.Context;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Config.core;
using Newtonsoft.Json;

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Config
{
    public static class CurrentConfig
    {
        public static CarRoot carConfig { get; private set; }
        public static CountryRoot countryConfig { get; private set; }
        private static AppDbContext _context;

        public static void SetDataBase(AppDbContext context)
        { 
            _context = context;
        }

        public static void GetCurrentConfig()
        {
            try
            {
                Entities.Config carConfigJSON = _context.Configs.Where(config => config.name == "CarConfig" && config.isActive).First();
                Entities.Config countryConfigJSON = _context.Configs.Where(config => config.name == "CountryConfig" && config.isActive).First();

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml
                };

                carConfig = JsonConvert.DeserializeObject<CarRoot>(carConfigJSON.data, settings);
                countryConfig = JsonConvert.DeserializeObject<CountryRoot>(countryConfigJSON.data, settings);
            }
            catch (Exception ex)
            {
                ErrorLogger.ErrorLogger.LogError("CurrentConfig -> GetCurrentConfig", ex.Message);
            }

        }
    }
}
