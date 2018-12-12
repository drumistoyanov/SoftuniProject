namespace GroceryStore.Web.Infrastructure
{
    public class ProductCountToBuy
    {
        public int Number { get; set; } = 1;

        public void Increase()
        {
            Number++;
        }

        public void Decrease()
        {
            Number--;
            if (Number <= 0)
            {
                Number = 1;
            }
        }
    }
}
