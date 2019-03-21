using AiWand.Core.Domain;
using AiWand.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Data.Mapping.Users
{
    public partial class UserMap : AiWandEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(attribute => attribute.Id);

            base.Configure(builder);
        }
    }
}
