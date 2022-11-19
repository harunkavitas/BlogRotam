using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogRotam.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Article { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public bool Check { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int Shows { get; set; }
        public virtual ICollection<Comment>comment { get; set; }

    }
}
