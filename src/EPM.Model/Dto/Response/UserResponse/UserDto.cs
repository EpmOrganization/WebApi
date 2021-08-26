using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.Dto.Response.UserResponse
{
    public class UserDto : User
    {
        public string RoleName { get; set; }

        public string DepartmentName { get; set; }
    }
}
