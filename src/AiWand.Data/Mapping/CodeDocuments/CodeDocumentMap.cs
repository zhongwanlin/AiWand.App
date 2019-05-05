﻿using AiWand.Core.Domain;
using AiWand.Core.Domain.CodeDocuments;
using AiWand.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace AiWand.Data.Mapping.CodeDocuments
{
    public partial class CodeDocumentMap : AiWandEntityTypeConfiguration<CodeDocument>
    {
        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<CodeDocument> builder)
        {
            builder.ToTable("CodeDocument");
            builder.HasKey(attribute => attribute.Id);

            base.Configure(builder);
        }
    }
}
