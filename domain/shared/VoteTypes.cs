namespace domain.shared;

public static class VoteTypes
{
    public static readonly short Upvote = 1; 
    public static readonly short DownVote = -1;

    public static bool IsValidVote(short vote)
    {
        return vote == Upvote || vote == DownVote;
    }
}




