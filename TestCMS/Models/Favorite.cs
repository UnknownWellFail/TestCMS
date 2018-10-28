namespace TestCMS.Models
{
    public class Favorite : BaseEntity
    {
        public string Nickname{ get; set; }
        public string AvatarPath { get; set; }
        public string Name { get; set; }
    }
}