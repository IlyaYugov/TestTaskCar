namespace CarApi.Models
{
    public class CarstoreDatabaseSettings : ICarstoreDatabaseSettings
    {
        public string CarsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICarstoreDatabaseSettings
    {
        string CarsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}