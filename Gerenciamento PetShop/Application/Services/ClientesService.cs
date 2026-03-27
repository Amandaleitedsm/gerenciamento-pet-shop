using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IFileStorageService _fileStorageService;
        public ClientesService(IClientesRepository clientesRepository, IFileStorageService fileStorageService)
        {
            _clientesRepository = clientesRepository;
            _fileStorageService = fileStorageService;
        }
        public void AdicionarCliente(ClientesViewModel clientesViewModel)
        {
            var cliente = new Clientes();
            if (clientesViewModel.Photo != null)
            {
                var filePath = Path.Combine("Storage", clientesViewModel.Photo.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    clientesViewModel.Photo.CopyTo(stream);
                }
                cliente = new Clientes(clientesViewModel.Cpf, clientesViewModel.Nome, clientesViewModel.DataNascimento, filePath);
            }

            cliente = new Clientes(clientesViewModel.Cpf, clientesViewModel.Nome, clientesViewModel.DataNascimento);

            //_clientesRepository.Add(cliente);
        }
        public byte[] Baixar(string cpf)
        {
            var cliente = _clientesRepository.Get(cpf);

            if (cliente == null)
                return Array.Empty<byte>();

            var dataBytes = _fileStorageService.LerArquivo(cliente.Photo);
            return dataBytes;
        }

        public List<Clientes> GetClientes(int pageNumber, int pageQuantity)
        {
            var clientes = _clientesRepository.Get(pageNumber, pageQuantity);
            return clientes;
        }
    }
}
