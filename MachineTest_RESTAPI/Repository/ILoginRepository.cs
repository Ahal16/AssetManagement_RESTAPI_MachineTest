using MachineTest_RESTAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace MachineTest_RESTAPI.Repository
{
    public interface ILoginRepository
    {
        #region  1 - User Registration
        public Task<UserRegistration> RegisterUser(UserRegistration userreg);
        #endregion

        #region  2 - Login Validation
        public Task<UserRegistration> UserValidation(string username, string password);
        #endregion

        #region  3 -  Get All Users
        public Task<ActionResult<IEnumerable<UserRegistration>>> GetAllUsers();
        #endregion

        #region  4 - Search By Id
        public Task<ActionResult<UserRegistration>> UserSearchById(int id);
        #endregion

        #region  5 - Login Update
        public Task<ActionResult<UserRegistration>> UpdateLogin(int id, UserRegistration userReg);
        #endregion

        #region  6 - Delete by id
        public JsonResult Deletelogin(int id);
        #endregion
    }
}

