using System.Collections.Generic;

namespace NewsAggregator.Domain.Sessions.Enums
{
    public class InteractionTypes : Enumeration
    {
        public static InteractionTypes VIEW = new InteractionTypes(0, "View", 1);
        public static InteractionTypes LIKE = new InteractionTypes(1, "Like", 2);
        public static InteractionTypes BOOKMARK = new InteractionTypes(2, "Bookmark", 2.5);
        public static InteractionTypes FOLLOW = new InteractionTypes(3, "Follow", 3);
        public static InteractionTypes CommentCreated = new InteractionTypes(4, "Comment created", 4);


        private InteractionTypes(int key, string name, double ponderation) : base(key, name) 
        {
            Ponderation = ponderation;
        }

        public double Ponderation { get; private set; }

        public static List<InteractionTypeLookup> InteractionTypeLookup = new List<InteractionTypeLookup>
        {
            new InteractionTypeLookup { Key = VIEW.Id, Ponderation = VIEW.Ponderation },
            new InteractionTypeLookup { Key = LIKE.Id, Ponderation = LIKE.Ponderation },
            new InteractionTypeLookup { Key = BOOKMARK.Id, Ponderation = BOOKMARK.Ponderation },
            new InteractionTypeLookup { Key = FOLLOW.Id, Ponderation = FOLLOW.Ponderation },
            new InteractionTypeLookup { Key = CommentCreated.Id, Ponderation = CommentCreated.Ponderation }
        };
    }

    public class InteractionTypeLookup
    {
        public int Key { get; set; }
        public double Ponderation { get; set; }
    }
}
