namespace TestCMS.Models
{
    public class Shop:BaseEntity
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Category{get;set;}
        public double X{get;set;}
        public double Y{get;set;}
    }
}