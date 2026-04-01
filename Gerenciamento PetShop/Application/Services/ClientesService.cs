using AutoMapper;
using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IPetsRepository _petsRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        public ClientesService(
            IClientesRepository clientesRepository, 
            IFileStorageService fileStorageService, 
            IPetsRepository petsRepository,
            IMapper mapper
        )
        {
            _clientesRepository = clientesRepository;
            _fileStorageService = fileStorageService;
            _petsRepository = petsRepository;
            _mapper = mapper;
        }
        public void AdicionarCliente(ClientesCreateViewModel clientesViewModel)
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

            _clientesRepository.Add(cliente);
        }
        public byte[] Baixar(int id)
        {
            var cliente = _clientesRepository.Get(id);

            if (cliente == null)
                return Array.Empty<byte>();

            var dataBytes = _fileStorageService.LerArquivo(cliente.Photo);
            if (dataBytes == null || dataBytes.Length == 0) return null;
            return dataBytes;
        }

        public List<ClientesResponse> GetClientes(int pageNumber, int pageQuantity)
        {
            var clientes = _clientesRepository.Get(pageNumber, pageQuantity);
            return _mapper.Map<List<ClientesResponse>>(clientes);
        }

        public ClientesResponse GetClienteById (int id)
        {
            var cliente = _clientesRepository.Get(id);
            if (cliente == null) return null;
            return _mapper.Map<ClientesResponse>(cliente);
        }

        public ClientesResponse AtualizarCliente(int id, ClientesUpdateViewModel clientesViewModel)
        {
            var cliente = _clientesRepository.Get(id);
            if (cliente == null) return null;
            if (clientesViewModel.Nome != null) cliente.Nome = clientesViewModel.Nome;
            if (clientesViewModel.DataNascimento != null) cliente.Data_Nascimento = clientesViewModel.DataNascimento;
            if (clientesViewModel.Photo != null)
            {
                var filePath = Path.Combine("Storage", clientesViewModel.Photo.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    clientesViewModel.Photo.CopyTo(stream);
                }
                cliente.Photo = filePath;
            }
            _clientesRepository.Update(cliente);
            return _mapper.Map<ClientesResponse>(cliente);
        }
    }
}
