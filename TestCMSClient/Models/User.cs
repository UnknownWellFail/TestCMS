using System;
using System.ComponentModel.DataAnnotations;

namespace TestCMS.Models
{
    public class User:BaseEntity
    {
        
        public int id{get;set;}
        public string nickname{get;set;}
        public string avatar_path{get;set;}
        public double raiting{get;set;}
        
    }
}