namespace DAL.Entities
{
    public class FavorName: BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Favor> Favors { get; set; } = new HashSet<Favor>();
    }
}
