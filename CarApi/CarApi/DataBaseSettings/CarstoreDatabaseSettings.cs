using DataAccess;

namespace CarApi.DataBaseSettings
{
    public class CarstoreDatabaseSettings : ICarstoreDatabaseSettings
    {
        public string CarsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}