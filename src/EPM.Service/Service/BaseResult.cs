using EPM.Framework.Helper;
using EPM.Model.ApiModel;
using EPM.Model.Enum;

namespace EPM.Service.Service
{
    public class BaseResult
    {
        public static ValidateResult ReturnSuccess()
        {
            return new ValidateResult()
            {
                Code = (int)CustomerCode.Success,
                Msg = EnumHelper.GetEnumDesc(CustomerCode.Success)
            };
        }

        public static ValidateResult ReturnFail()
        {
            return new ValidateResult()
            {
                Code = (int)CustomerCode.Fail,
                Msg = EnumHelper.GetEnumDesc(CustomerCode.Fail)
            };
        }

        public static ValidateResult RoleAllot()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "该角色已经分配给用户"
            };
        }

        public static ValidateResult MenuHasBeenAssigned()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "该菜单已被分配给某些角色，请取消分配后再进行删除"
            };
        }

        public static ValidateResult ExistRole()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "已存在同名的角色，修改角色失败"
            };
        }

        public static ValidateResult ExistDepartment()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "已存在同名的部门名称，修改失败"
            };
        }
    }
}
