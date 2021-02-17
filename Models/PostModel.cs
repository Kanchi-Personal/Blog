using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace BlogApi.Models
{
    public class PostModel : BriefAuditBaseEntity
    {
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }

    }
}
