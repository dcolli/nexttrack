using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eftesting.Models
{
    public class Stream
    {
        public int SteamId { get; set; }
        public string Name { get; set; }
        
        //TODO: how can we implement these DateTime fields with default values instead of having to set them manually?
        public DateTime DateCreated { get; set; }
        
        [Timestamp]
        public Byte[] Timestamp { get; set; } //should we be doing this or is it obsolete?
    }
}