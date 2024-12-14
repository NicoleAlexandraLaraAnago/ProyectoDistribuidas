using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserLogic
    {
        // Registrar un nuevo usuario
        public Users CreateUser(Users newUser)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Verificar si el usuario ya existe
                Users existingUser = r.Retrieve<Users>(u => u.Username == newUser.Username);
                if (existingUser != null)
                {
                    throw new Exception("Username already exists.");
                }

                // Crear nuevo usuario
                var result = r.Create(newUser);
                return result;
            }
        }

        // Autenticar usuario
        public bool AuthenticateUser(string username, string password)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Verificar si el usuario existe
                var user = r.Retrieve<Users>(u => u.Username == username);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                // Comparar contraseñas (recuerda que debes almacenar y comparar los hashes)
                bool isPasswordValid = ComparePasswordHash(password, user.PasswordHash);
                return isPasswordValid;
            }
        }

        // Comparar el hash de la contraseña (debes implementar esta función de acuerdo a tu método de hash)
        private bool ComparePasswordHash(string password, byte[] storedHash)
        {
            // Convertir el hash almacenado en un string, ya que BCrypt trabaja con strings.
            string storedHashString = Convert.ToBase64String(storedHash);

            // Usar BCrypt para verificar si la contraseña coincide con el hash
            bool isMatch = BCrypt.Net.BCrypt.Verify(password, storedHashString);

            return isMatch;
        }
        public void AssignRole(int userID, int roleID)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var userRole = new UserRoles
                {
                    UserID = userID,
                    RoleID = roleID
                };
                r.Create(userRole);
            }
        }
    }
}
