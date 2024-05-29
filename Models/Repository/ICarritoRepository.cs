namespace Amazon.Models.Repository
{
    public interface ICarritoRepository
    {
        public IEnumerable<Carrito> GetCartsWithUsers(int userID);
        public void AddCart(Carrito cart);
    }
}
