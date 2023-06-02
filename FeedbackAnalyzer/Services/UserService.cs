using MongoDB.Driver;
using Prediction.Entities;
using Prediction.Interfaces;

namespace Prediction.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        //public UserService()
        //{
        //    var client = new MongoClient(settings.ConnectionString);
        //    var database = client.GetDatabase(settings.DatabaseName);

        //    _users = database.GetCollection<User>(settings.EmployeesCollectionName);

        //}
    }
}
