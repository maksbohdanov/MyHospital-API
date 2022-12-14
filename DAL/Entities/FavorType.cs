namespace DAL.Entities
{
    public class FavorType : BaseEntity
    {
        public string Type { get; set; } = string.Empty;

        public virtual ICollection<Favor> Favors { get; set; } = new HashSet<Favor>();
    }
}
