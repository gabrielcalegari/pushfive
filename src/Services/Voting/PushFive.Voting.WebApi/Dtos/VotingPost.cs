namespace PushFive.Voting.WebApi.Dtos
{
    public class VotingPost
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public VotingItemDto[] Items { get; set; }
    }
}
