namespace GroceryStore.Web.Infrastructure
{
    public class ProductCountToBuy
    {
        public int Number { get; set; } = 1;

        public void Increase()
        {
            this.Number++;
        }

        public void Decrease()
        {
            this.Number--;
            if (this.Number <= 0)
            {
                this.Number = 1;
            }
        }
    }
}
