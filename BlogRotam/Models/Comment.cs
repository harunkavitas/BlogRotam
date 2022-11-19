using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogRotam.Models
{
    public class Comment
    {
        
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CommentDate { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }


    }
}
