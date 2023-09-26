﻿using ServerPagination.Models;
using ServerPagination.StoredProcedureDbAccess;

namespace ServerPagination.DataAccess.StoredProcedureDbAccess.Abstraction
{
    public interface IHomeDbRepository : IGenericRepository<UserModel>
    {
        void ActiveManage(int userId);
        void AddUser(UserModel user);
        void DeleteUser(int userId);
        void EditUser(UserModel user);
        UserModel GetUser(int userId);
        PaginationModel UserList(int pageNumber, string searchQuery);
    }
}
