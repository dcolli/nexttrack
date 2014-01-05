using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eftesting.Models
{
    /// <summary>
    /// Model for the room - codefirst 
    /// - all the properties should be dynamically ORM'd from the db
    /// - assume a local sql server or sql compact
    /// 
    /// </summary>
    /// 
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }        
        
        //TODO: how can we implement these DateTime fields with default values instead of having to set them manually?
        //public DateTime CreatedDate { get; set; }
        //public DateTime LastModifiedDate { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; } //should we be doing this or is it obsolete?

        //public virtual ICollection<Stream> Streams { get; set; }
    }
}