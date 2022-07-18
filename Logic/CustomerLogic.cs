namespace ChealCore.Logic
{
    public class CustomerLogic
    {
        string customerId = "0";

        Random random = new Random();   

        public string GenerateCustomerId()
        {
            for(int i = 0; i < 7; i++)
            {
                customerId += random.Next(1,9).ToString();
             
            }
            return customerId;
        }
    }
}
