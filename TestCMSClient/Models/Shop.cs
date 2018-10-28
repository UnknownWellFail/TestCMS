namespace TestCMS.Models
{
    public class Shop:BaseEntity
    {
        public int id{get;set;}
        public string name{get;set;}
        public string category{get;set;}
        public double x{get;set;}
        public double y{get;set;}
    }
}