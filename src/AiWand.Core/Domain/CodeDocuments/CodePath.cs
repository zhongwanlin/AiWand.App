using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AiWand.Core.Domain.CodeDocuments
{
    public class CodePath
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [StringLength(32)]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
    }
}
