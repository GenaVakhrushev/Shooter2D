namespace Shooter.Inventory.Items
{
    public abstract class Item
    {
        public string Name { get; private set; }

        protected Item(string name)
        {
            Name = name;
        }

        public abstract void Use();
    }
}