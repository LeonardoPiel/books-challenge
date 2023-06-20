using API.Models;
using System.Text.Json;

namespace API.Services
{
    public class DefaultService
    {
        private readonly IConfiguration _configuration;
        public List<string> Errors { get; set; }
        public DefaultService(IConfiguration configuration)
        {
            _configuration = configuration;
            Errors = new List<string>();
        }

        public string GetJsonContent(string fileName)
        {
            try
            {
                var dataFolderPath = _configuration.GetValue<string>("DataFolderPath");
                var filePath = Path.Combine(dataFolderPath, fileName);

                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao ler o arquivo JSON.", ex);
            }

            return string.Empty;
        }
    }
}
