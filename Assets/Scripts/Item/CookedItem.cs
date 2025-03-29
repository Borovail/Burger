namespace Item
{
    public class CookedItem
    {
        private Item item;
        private bool isCooked;

        public Item Item => item;
        public bool IsCooked => isCooked;
        
        public CookedItem(Item item)
        {
            this.item = item;
            isCooked = false;
        }

        public void Cook()
        {
            isCooked = true;
        }
    }
}