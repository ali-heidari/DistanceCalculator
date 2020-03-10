
namespace Core.Data
{
    public class User : DataObject
    {
        public override string DocumentName { get => "User"; }
        public string email{get;set;}
        public string username{get;set;}
        public string password{get;set;}
    }
}