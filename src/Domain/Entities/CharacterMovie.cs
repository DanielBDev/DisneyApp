namespace Domain.Entities
{
    public class CharacterMovie
    {
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
