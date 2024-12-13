using MachineTest_RESTAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineTest_RESTAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MachineTestAssetMngtContext _context;

        public LoginRepository(MachineTestAssetMngtContext context)
        {
            _context = context;
        }

        #region  1 - User Registration
        public async Task<UserRegistration> RegisterUser(UserRegistration userreg)
        {
        //    if (userreg == null)
        //    {
        //        throw new ArgumentNullException(nameof(userreg), "UserRegistration is null");
        //    }
            try
            {
                await _context.UserRegistrations.AddAsync(userreg);
                await _context.SaveChangesAsync();

                var regUser = await _context.UserRegistrations
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserId == userreg.UserId);

                return regUser;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while registering...", ex);
            }
            catch (Exception ex)
            {

                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        #endregion

        #region  2 - Login Validation
        public async Task<UserRegistration> UserValidation(string userName, string passWord)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord))
            {
                throw new ArgumentException("Username / Password is invalid.");
            }

            try
            {
                var user = await _context.UserRegistrations
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == userName && u.Password == passWord);

                return user;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while validating...", ex);
            }
        }
        #endregion

        #region  3 -  Get All Users
        public async Task<ActionResult<IEnumerable<UserRegistration>>> GetAllUsers()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.UserRegistrations.Include(ro => ro.Role).ToListAsync();
                }

                return new List<UserRegistration>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  4 - Search By Id
        public async Task<ActionResult<UserRegistration>> UserSearchById(int id)
        {
            try
            {
                if (_context != null)
                {

                    var userlog = await _context.UserRegistrations
                    .Include(role => role.Role)
                    .FirstOrDefaultAsync(us => us.UserId == id);
                    return userlog;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  5 - Login Update
        public async Task<ActionResult<UserRegistration>> UpdateLogin(int id, UserRegistration userRegis)
        {
            try
            {
                if (userRegis == null)
                {
                    throw new ArgumentNullException(nameof(userRegis), "Existing user data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                var existingUsers = await _context.UserRegistrations.FindAsync(id);
                if (existingUsers == null)
                {
                    return null;
                }

                existingUsers.Age = userRegis.Age;
                existingUsers.FullName = userRegis.FullName;
                existingUsers.Address = userRegis.Address;
                existingUsers.Username = userRegis.Username;
                existingUsers.Password = userRegis.Password;

                await _context.SaveChangesAsync();

                var login = await _context.UserRegistrations.Include(r => r.Role).FirstOrDefaultAsync(l => l.UserId == userRegis.UserId);
                return login;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  6 - Delete by id
        public JsonResult Deletelogin(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid UserId"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }
               
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
              
                var existingUser = _context.UserRegistrations.Find(id);
                if (existingUser == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "User  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                _context.UserRegistrations.Remove(existingUser);

                _context.SaveChangesAsync();

                return new JsonResult(new
                {
                    success = true,
                    message = "Login Credential  deleted successfully"

                })
                {
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            #endregion
        }
    }
}

