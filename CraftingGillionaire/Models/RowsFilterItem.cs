namespace CraftingGillionaire.Models
{
    public class RowsFilterItem
    {
        public RowsFilterItem(string name, int id) 
        { 
            this.Name = name;
            this.ID = id;
        }

        public string Name { get; }

        public int ID { get; }
    }
}
