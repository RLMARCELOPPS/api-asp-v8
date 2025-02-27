using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Content must be 10 characters")]
        [MaxLength(5000, ErrorMessage = "Content cannot be over 5000 characters")]
        public string Content { get; set; } = string.Empty;
    }
}