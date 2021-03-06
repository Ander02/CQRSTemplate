﻿using System;

namespace CQRSTemplate.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
