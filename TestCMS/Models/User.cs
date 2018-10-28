using System;
using System.ComponentModel.DataAnnotations;

namespace TestCMS.Models
{
    public class User:BaseEntity
    {
        
        public int Id{get;set;}
        public string Nickname{get;set;}
        public string AvatarPath{get;set;}
        public double Raiting{get;set;}
        
    }
}