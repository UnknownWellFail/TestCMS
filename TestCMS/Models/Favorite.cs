namespace TestCMS.Models
{
    public class Favorite : BaseEntity
    {
      
        public string nickname{ get; set; }
        public string avatar_path { get; set; }
        public string name { get; set; }
    }
}