﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Models.Notifications;

namespace WeLearn.Data.Models;

public class Comment : BaseEntity
{
    public Comment()
    {
    }

    public Comment(string body, Guid authorId, Guid contentId)
    {
        Id = Guid.NewGuid(); // TODO move to context savechanges async - if created, set Id
        Body = body;
        AuthorId = authorId;
        ContentId = contentId;
    }

    public string Body { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ContentId { get; set; }

    public virtual Account Author { get; set; }
    public virtual Content.Content Content { get; set; }
    public virtual ICollection<CommentNotification> CommentNotifications { get; set; }
}
