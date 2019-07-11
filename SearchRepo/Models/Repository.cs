using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchRepo.Models
{


    public class Repository
    {
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public List<Item> Items { get; set; }
    }
    public class Item
    {
        public string Name { get; set; }
        public Owner Owner { get; set; }
    }
    public class Owner
    {
        public string Avatar_url { get; set; }
    }
}