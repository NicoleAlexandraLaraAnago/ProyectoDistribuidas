using BLL;
using Entities;
using System;
using System.Threading.Tasks;

namespace SLC
{
public interface IUserService
    {
        Task<Users> CreateUser(Users newUser);
        Task<bool> AuthenticateUser(string username, string password);
        Task AssignRole(int userID, int roleID);
    }

    public class UserService : IUserService
    {
        private readonly UserLogic _userLogic;

        // Constructor que inicializa la capa de lógica de negocio
        public UserService()
        {
            _userLogic = new UserLogic();
        }

        // Método para registrar un nuevo usuario
        public async Task<Users> CreateUser(Users newUser)
        {
            try
            {
                return await Task.Run(() => _userLogic.CreateUser(newUser));
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si el usuario ya existe, etc.
                throw new Exception("Error creating user: " + ex.Message);
            }
        }

        // Método para autenticar un usuario
        public async Task<bool> AuthenticateUser(string username, string password)
        {
            try
            {
                return await Task.Run(() => _userLogic.AuthenticateUser(username, password));
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user: " + ex.Message);
            }
        }

        // Método para asignar un rol a un usuario
        public async Task AssignRole(int userID, int roleID)
        {
            try
            {
                await Task.Run(() => _userLogic.AssignRole(userID, roleID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error assigning role: " + ex.Message);
            }
        }
    }
}
