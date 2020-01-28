namespace PushFive.Voting.WebApi.Dtos
{
    public class VoterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Quantity of hits the voter got
        /// </summary>
        public int Hit { get; set; }
    }
}
