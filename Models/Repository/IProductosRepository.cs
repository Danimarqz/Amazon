namespace Amazon.Models.Repository
{
    public interface IProductosRepository
    {
        public Task<IEnumerable<Productos>> GetAll();
        public Task<Productos> GetById(int id);
        public void Create(Productos productos);
        public void Update(Productos productos);
        public void Delete(int id);

    }
}
