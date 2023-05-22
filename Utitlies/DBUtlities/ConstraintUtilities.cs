namespace WatchStock.Utitlies.DBUtlities
{
    public class ConstraintUtilities
    {
        public static int GeneratePrimaryKey()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }
    }
}
