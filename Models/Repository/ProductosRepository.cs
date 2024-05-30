
namespace Amazon.Models.Repository
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly Conexion _conexion;

        public ProductosRepository(Conexion conexion)
        {
            _conexion = conexion;
        }
        public void Create(Productos productos)
        {
            throw new NotImplementedException();
        }

        public void Delete(Productos productos)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Productos>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Productos> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Productos productos)
        {
            throw new NotImplementedException();
        }
    }
}
