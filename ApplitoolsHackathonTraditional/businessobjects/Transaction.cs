namespace ApplitoolsHackathonTraditional.businessobjects
{
    /// <summary>
    /// Custom object for making validations against the Recent Transactions tale of the homepage easier
    /// </summary>
    public class Transaction
    {
        public decimal amount;
        public string description;
        public string category;
        public string status;
        public string transactionDateTime;

        /// <summary>
        /// Custom Hash Code method in order to use default comparators
        /// </summary>
        /// <returns>a hashcode</returns>
        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + amount.GetHashCode();
            hashCode = hashCode * -1521134295 + description.GetHashCode();
            hashCode = hashCode * -1521134295 + category.GetHashCode();
            hashCode = hashCode * -1521134295 + status.GetHashCode();
            hashCode = hashCode * -1521134295 + transactionDateTime.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Custom Equals method in order to use the default comparators
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>True if the provided Transaction is equal to this one, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Transaction))
            {
                return false;
            }

            var other = (Transaction) obj;
            return amount == other.amount && description.Equals(other.description) && category.Equals(other.category) &&
                   status.Equals(other.status) && transactionDateTime.Equals(other.transactionDateTime);
        }
    }
}
